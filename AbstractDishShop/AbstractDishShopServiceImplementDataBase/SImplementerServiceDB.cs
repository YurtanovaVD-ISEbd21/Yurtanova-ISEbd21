using AbstractDishShopModel;
using AbstractDishShopServiceDAL.BindingModels;
using AbstractDishShopServiceDAL.Interfaces;
using AbstractDishShopServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
namespace AbstractDishShopServiceImplementDataBase.Implementations
{
    public class ImplementerServiceDB : IImplementerService
    {
        private AbstractDbContext context;
        public ImplementerServiceDB(AbstractDbContext context)
        {
            this.context = context;
        }
        public List<ImplementerViewModel> GetList()
        {
            List<ImplementerViewModel> result = context.Implementers
            .Select(rec => new ImplementerViewModel
            {
                Id = rec.Id,
                ImplementerName = rec.ImplementerName
            })
            .ToList();
            return result;
        }
        public ImplementerViewModel GetElement(int id)
        {
            Implementer element = context.Implementers.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new ImplementerViewModel
                {
                    Id = element.Id,
                    ImplementerName = element.ImplementerName
                };
            }
            throw new Exception("Элемент не найден");
        }
        public void AddElement(ImplementerBindingModel model)
        {
            Implementer element = context.Implementers.FirstOrDefault(rec => rec.ImplementerName == model.ImplementerName);
            if (element != null)
            {
                throw new Exception("Уже есть сотрудник с таким ФИО");
            }
            context.Implementers.Add(new Implementer
            {
                ImplementerName = model.ImplementerName
            });
            context.SaveChanges();
        }
        public void UpdElement(ImplementerBindingModel model)
        {
            Implementer element = context.Implementers.FirstOrDefault(rec =>
            rec.ImplementerName == model.ImplementerName && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть сотрудник с таким ФИО");
            }
          
        element = context.Implementers.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.ImplementerName = model.ImplementerName;
            context.SaveChanges();
        }
        public void DelElement(int id)
        {
            Implementer element = context.Implementers.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                context.Implementers.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
        public ImplementerViewModel GetFreeImplementer()
        {
            var ordersWorker = context.Implementers
            .Select(x => new
            {
                ImplId = x.Id,
                Count = context.SOrders.Where(o => o.Status == SOrderStatus.Выполняется && o.ImplementerId == x.Id).Count()
            })
            .OrderBy(x => x.Count)
            .FirstOrDefault();
            if (ordersWorker != null)
            {
                return GetElement(ordersWorker.ImplId);
            }
            return null;
        }
    }
}
