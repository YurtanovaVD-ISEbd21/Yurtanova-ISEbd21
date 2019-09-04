using AbstractDishShopModel;
using AbstractDishShopServiceDAL.BindingModels;
using AbstractDishShopServiceDAL.Interfaces;
using AbstractDishShopServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;


namespace AbstractDishShopServiceImplementList.Implementations
{
    public class MainServiceList : IMainService
    {
        private DataListSingleton source;
        public MainServiceList()
        {
            source = DataListSingleton.GetInstance();
        }
        public List<SOrderViewModel> GetList()
        {
            List<SOrderViewModel> result = source.Orders
            .Select(rec => new SOrderViewModel
            {
                Id = rec.Id,
                SClientId = rec.SClientId,
                DishId = rec.DishId,
                DateCreate = rec.DateCreate.ToLongDateString(),
                DateImplement = rec.DateImplement?.ToLongDateString(),
                Status = rec.Status.ToString(),
                Count = rec.Count,
                Sum = rec.Sum,
                SClientFIO = source.Clients.FirstOrDefault(recC => recC.Id == rec.SClientId)?.SClientFIO,
                DishName = source.Dishs.FirstOrDefault(recP => recP.Id == rec.DishId)?.DishName,
            })
            .ToList();
            return result;
        }
        
        public void CreateSOrder(SOrderBindingModel model)
        {
            int maxId = source.Orders.Count > 0 ? source.Orders.Max(rec => rec.Id) : 0;
            source.Orders.Add(new SOrder
            {
                Id = maxId + 1,
                SClientId = model.SClientId,
                DishId = model.DishId,
                DateCreate = DateTime.Now,
                Count = model.Count,
                Sum = model.Sum,
                Status = SOrderStatus.Принят
            });
        }
        public void TakeSOrderInWork(SOrderBindingModel model)
        {
            SOrder element = source.Orders.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            if (element.Status != SOrderStatus.Принят)
            {
                throw new Exception("Заказ не в статусе \"Принят\"");
            }
            // смотрим по количеству компонентов на складах
            var DishMaterialss = source.DishMaterialss.Where(rec => rec.DishId == element.DishId);
          
        foreach (var DishMaterials in DishMaterialss)
            {
                int countOnStocks = source.StockMaterialss
                .Where(rec => rec.MaterialsId == DishMaterials.MaterialsId)
                .Sum(rec => rec.Count);
                if (countOnStocks < DishMaterials.Count * element.Count)
                {
                    var MaterialsName = source.Materialss.FirstOrDefault(rec => rec.Id == DishMaterials.MaterialsId);
                    throw new Exception("Не достаточно компонента " + MaterialsName?.MaterialsName + " требуется " + (DishMaterials.Count * element.Count) + ", в наличии " + countOnStocks);
                }
            }
            // списываем
            foreach (var DishMaterials in DishMaterialss)
            {
                int countOnStocks = DishMaterials.Count * element.Count;
                var stockMaterialss = source.StockMaterialss.Where(rec => rec.MaterialsId == DishMaterials.MaterialsId);
                foreach (var stockMaterials in stockMaterialss)
                {
                    // компонентов на одном слкаде может не хватать
                    if (stockMaterials.Count >= countOnStocks)
                    {
                        stockMaterials.Count -= countOnStocks;
                        break;
                    }
                    else
                    {
                        countOnStocks -= stockMaterials.Count;
                        stockMaterials.Count = 0;
                    }
                }
            }
            element.DateImplement = DateTime.Now;
            element.Status = SOrderStatus.Выполняется;
        }
        public void FinishSOrder(SOrderBindingModel model)
        {
            SOrder element = source.Orders.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            if (element.Status != SOrderStatus.Выполняется)
            {
                throw new Exception("Заказ не в статусе \"Выполняется\"");
            }
            element.Status = SOrderStatus.Готов;
        }
        public void PaySOrder(SOrderBindingModel model)
        {
            SOrder element = source.Orders.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            if (element.Status != SOrderStatus.Готов)
            {
                throw new Exception("Заказ не в статусе \"Готов\"");
                
            }
            element.Status = SOrderStatus.Оплачен;
        }
        public void PutMaterialsOnStock(StockMaterialsBindingModel model)
        {
            StockMaterials element = source.StockMaterialss.FirstOrDefault(rec => rec.StockId == model.StockId && rec.MaterialsId == model.MaterialsId);
            if (element != null)
            {
                element.Count += model.Count;
            }
            else
            {
                int maxId = source.StockMaterialss.Count > 0 ? source.StockMaterialss.Max(rec => rec.Id) : 0;
                source.StockMaterialss.Add(new StockMaterials
                {
                    Id = ++maxId,
                    StockId = model.StockId,
                    MaterialsId = model.MaterialsId,
                    Count = model.Count
                });
            }
        }

        public List<SOrderViewModel> GetFreeSOrders()
        {
            throw new NotImplementedException();
        }
    }
}