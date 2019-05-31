using AbstractDishShopModel;
using AbstractDishShopServiceDAL.BindingModels;
using AbstractDishShopServiceDAL.Interfaces;
using AbstractDishShopServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractDishShopServiceImplementDataBase
{
    public class StockServiceBD : IStockService
    {
        private AbstractDbContext context;

        public StockServiceBD(AbstractDbContext context)
        {
            this.context = context;
        }

        public List<StockViewModel> GetList()
        {
            List<StockViewModel> result = context.Stocks
                .Select(rec => new StockViewModel
                {
                    Id = rec.Id,
                    StockName = rec.StockName,
                    StockMaterialss = context.StockMaterialss
                            .Where(recPC => recPC.StockId == rec.Id)
                            .Select(recPC => new StockMaterialsViewModel
                            {
                                Id = recPC.Id,
                                StockId = recPC.StockId,
                                MaterialsId = recPC.MaterialsId,
                                MaterialsName = recPC.Materials.MaterialsName,
                                Count = recPC.Count
                            })
                            .ToList()
                })
                .ToList();
            return result;
        }

        public StockViewModel GetElement(int id)
        {
            Stock element = context.Stocks.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new StockViewModel
                {
                    Id = element.Id,
                    StockName = element.StockName,
                    StockMaterialss = context.StockMaterialss
                            .Where(recPC => recPC.StockId == element.Id)
                            .Select(recPC => new StockMaterialsViewModel
                            {
                                Id = recPC.Id,
                                StockId = recPC.StockId,
                                MaterialsId = recPC.MaterialsId,
                                MaterialsName = recPC.Materials.MaterialsName,
                                Count = recPC.Count
                            })
                            .ToList()
                };
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(StockBindingModel model)
        {
            Stock element = context.Stocks.FirstOrDefault(rec => rec.StockName == model.StockName);
            if (element != null)
            {
                throw new Exception("Уже есть склад с таким названием");
            }
            context.Stocks.Add(new Stock
            {
                StockName = model.StockName
            });
            context.SaveChanges();
        }

        public void UpdElement(StockBindingModel model)
        {
            Stock element = context.Stocks.FirstOrDefault(rec =>
                                        rec.StockName == model.StockName && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть склад с таким названием");
            }
            element = context.Stocks.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.StockName = model.StockName;
            context.SaveChanges();
        }

        public void DelElement(int id)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Stock element = context.Stocks.FirstOrDefault(rec => rec.Id == id);
                    if (element != null)
                    {
                        // при удалении удаляем все записи о компонентах на удаляемом складе
                        context.StockMaterialss.RemoveRange(
                                            context.StockMaterialss.Where(rec => rec.StockId == id));
                        context.Stocks.Remove(element);
                        context.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("Элемент не найден");
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}