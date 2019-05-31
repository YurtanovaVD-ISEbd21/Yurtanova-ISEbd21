using AbstractDishShopModel;
using AbstractDishShopServiceDAL.BindingModels;
using AbstractDishShopServiceDAL.Interfaces;
using AbstractDishShopServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;

namespace AbstractDishShopServiceImplementDataBase.Implementations
{
    public class MainServiceDB : IMainService
    {
        private AbstractDbContext context;
        public MainServiceDB(AbstractDbContext context)
        {
            this.context = context;
        }
        public List<SOrderViewModel> GetList()
        {
            List<SOrderViewModel> result = context.SOrders.Select(rec => new SOrderViewModel
            {
                Id = rec.Id,
                SClientId = rec.SClientId,
                DishId = rec.DishId,
                DateCreate = SqlFunctions.DateName("dd", rec.DateCreate) + " " +
            SqlFunctions.DateName("mm", rec.DateCreate) + " " +
            SqlFunctions.DateName("yyyy", rec.DateCreate),
                DateImplement = rec.DateImplement == null ? "" :
            SqlFunctions.DateName("dd", rec.DateImplement.Value) + " " +
            SqlFunctions.DateName("mm", rec.DateImplement.Value) + " " +
            SqlFunctions.DateName("yyyy", rec.DateImplement.Value),
                Status = rec.Status.ToString(),
                Count = rec.Count,
                Sum = rec.Sum,
                SClientFIO = rec.SClient.SClientFIO,
                DishName = rec.Dish.DishName
            })
            .ToList();
            return result;
        }
        public void CreateSOrder(SOrderBindingModel model)
        {
            context.SOrders.Add(new SOrder
            {
                SClientId = model.SClientId,
                DishId = model.DishId,
                DateCreate = DateTime.Now,
                Count = model.Count,
                Sum = model.Sum,
                Status = SOrderStatus.Принят
            });
            context.SaveChanges();
        }
        public void TakeSOrderInWork(SOrderBindingModel model)
        {
            
        using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    SOrder element = context.SOrders.FirstOrDefault(rec => rec.Id == model.Id);
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                    if (element.Status != SOrderStatus.Принят)
                    {
                        throw new Exception("Заказ не в статусе \"Принят\"");
                    }
                    var dishMaterialss = context.DishMaterialss.Include(rec => rec.Materials).Where(rec => rec.DishId == element.DishId);
                    // списываем
                    foreach (var DishMaterials in dishMaterialss)
                    {
                        int countOnStocks = DishMaterials.Count * element.Count;
                        var stockMaterialss = context.StockMaterialss.Where(rec => rec.MaterialsId == DishMaterials.MaterialsId);
                        foreach (var stockMaterials in stockMaterialss)
                        {
                            // компонентов на одном слкаде может не хватать
                            if (stockMaterials.Count >= countOnStocks)
                            {
                                stockMaterials.Count -= countOnStocks;
                                countOnStocks = 0;
                                context.SaveChanges();
                                break;
                            }
                            else
                            {
                                countOnStocks -= stockMaterials.Count;
                                stockMaterials.Count = 0;
                                context.SaveChanges();
                            }
                        }
                        if (countOnStocks > 0)
                        {
                            throw new Exception("Не достаточно компонента " + DishMaterials.Materials.MaterialsName + " требуется " + DishMaterials.Count + ", не хватает " + countOnStocks);
                        }
                    }
                    element.DateImplement = DateTime.Now;
                    element.Status = SOrderStatus.Выполняется;
                    context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
        public void FinishSOrder(SOrderBindingModel model)
        {
            SOrder element = context.SOrders.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
               
        {
                throw new Exception("Элемент не найден");
            }
            if (element.Status != SOrderStatus.Выполняется)
            {
                throw new Exception("Заказ не в статусе \"Выполняется\"");
            }
            element.Status = SOrderStatus.Готов;
            context.SaveChanges();
        }
        public void PaySOrder(SOrderBindingModel model)
        {
            SOrder element = context.SOrders.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            if (element.Status != SOrderStatus.Готов)
            {
                throw new Exception("Заказ не в статусе \"Готов\"");
            }
            element.Status = SOrderStatus.Оплачен;
            context.SaveChanges();
        }
        public void PutMaterialsOnStock(StockMaterialsBindingModel model)
        {
            StockMaterials element = context.StockMaterialss.FirstOrDefault(rec => rec.StockId == model.StockId && rec.MaterialsId == model.MaterialsId);
            if (element != null)
            {
                element.Count += model.Count;
            }
            else
            {
                context.StockMaterialss.Add(new StockMaterials
                {
                    StockId = model.StockId,
                    MaterialsId = model.MaterialsId,
                    Count = model.Count
                });
            }
            context.SaveChanges();
        }
    }
}