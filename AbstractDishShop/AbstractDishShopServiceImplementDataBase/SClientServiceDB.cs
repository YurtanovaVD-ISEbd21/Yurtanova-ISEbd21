using AbstractDishShopModel;
using AbstractDishShopServiceDAL.BindingModels;
using AbstractDishShopServiceDAL.Interfaces;
using AbstractDishShopServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AbstractDishShopServiceImplementDataBase.Implementations
{

    public class SClientServiceDB : ISClientService
    {
        private AbstractDbContext context;
        public SClientServiceDB(AbstractDbContext context)
        {
            this.context = context;
        }
        public List<SClientViewModel> GetList()
        {
            List<SClientViewModel> result = context.SClients.Select(rec => new SClientViewModel
            {
                Id = rec.Id,
                SClientFIO = rec.SClientFIO,
                Mail = rec.Mail
            })
            .ToList();
            return result;
        }
        public SClientViewModel GetElement(int id)
        {
            SClient element = context.SClients.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new SClientViewModel
                {
                    Id = element.Id,
                    SClientFIO = element.SClientFIO,
                    Mail = element.Mail,
                    Messages = context.MessageInfos
                .Where(recM => recM.SClientId == element.Id)
                .Select(recM => new MessageInfoViewModel
                {
                    MessageId = recM.MessageId,
                    DateDelivery = recM.DateDelivery,
                    Subject = recM.Subject,
                    Body = recM.Body
                })
                .ToList()
                };
            }
            throw new Exception("Элемент не найден");
        }
        public void AddElement(SClientBindingModel model)
        {
            SClient element = context.SClients.FirstOrDefault(rec => rec.SClientFIO == model.SClientFIO);
            if (element != null)
            {
                throw new Exception("Уже есть клиент с таким ФИО");
               
            }
            context.SClients.Add(new SClient
            {
                SClientFIO = model.SClientFIO,
                Mail = model.Mail
            });
            context.SaveChanges();
        }
        public void UpdElement(SClientBindingModel model)
        {
            SClient element = context.SClients.FirstOrDefault(rec => rec.SClientFIO == model.SClientFIO && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть клиент с таким ФИО");
            }
            element = context.SClients.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.SClientFIO = model.SClientFIO;
            element.Mail = model.Mail;
            context.SaveChanges();
        }
        public void DelElement(int id)
        {
            SClient element = context.SClients.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                context.SClients.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}