using AbstractDishShopModel;
using AbstractDishShopServiceDAL.BindingModels;
using AbstractDishShopServiceDAL.Interfaces;
using AbstractDishShopServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractDishShopServiceImplementList.Implementations
{
    public class SClientServiceList : ISClientService
    {
        private DataListSingleton source;
        public SClientServiceList()
        {
            source = DataListSingleton.GetInstance();
        }
        public List<SClientViewModel> GetList()
        {
            
        List<SClientViewModel> result = source.Clients.Select(rec => new SClientViewModel
        {
            Id = rec.Id,
            SClientFIO = rec.SClientFIO
        })
        .ToList();
            return result;
        }
        public SClientViewModel GetElement(int id)
        {
            SClient element = source.Clients.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new SClientViewModel
                {
                    Id = element.Id,
                    SClientFIO = element.SClientFIO
                };
            }
            throw new Exception("Элемент не найден");
        }
        public void AddElement(SClientBindingModel model)
        {
            SClient element = source.Clients.FirstOrDefault(rec => rec.SClientFIO == model.SClientFIO);
            if (element != null)
            {
                throw new Exception("Уже есть клиент с таким ФИО");
            }
            int maxId = source.Clients.Count > 0 ? source.Clients.Max(rec => rec.Id) : 0;
            source.Clients.Add(new SClient
            {
                Id = maxId + 1,
                SClientFIO = model.SClientFIO
            });
        }
        public void UpdElement(SClientBindingModel model)
        {
            SClient element = source.Clients.FirstOrDefault(rec => rec.SClientFIO == model.SClientFIO && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть клиент с таким ФИО");
            }
            element = source.Clients.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.SClientFIO = model.SClientFIO;
        }
        public void DelElement(int id)
        {
            SClient element = source.Clients.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                source.Clients.Remove(element);
            }
            else
               
        {
                throw new Exception("Элемент не найден");
            }
        }
    }
}