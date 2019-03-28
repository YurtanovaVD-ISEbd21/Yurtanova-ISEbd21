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
    public class ClientServiceList : ISClientService
    {
        private DataListSingleton source;
        public ClientServiceList()
        {
            source = DataListSingleton.GetInstance();
        }
        public List<SClientViewModel> GetList()
        {
            List<SClientViewModel> result = new List<SClientViewModel>();
            for (int i = 0; i < source.SClients.Count; ++i)
            {
                result.Add(new SClientViewModel
                {
                    Id = source.SClients[i].Id,
                    SClientFIO = source.SClients[i].SClientFIO
                });
            }

            return result;
        }
        public SClientViewModel GetElement(int id)
        {
            for (int i = 0; i < source.SClients.Count; ++i)
            {
                if (source.SClients[i].Id == id)
                {
                    return new SClientViewModel
                    {
                        Id = source.SClients[i].Id,
                        SClientFIO = source.SClients[i].SClientFIO
                    };
                }
            }
            throw new Exception("Элемент не найден");
        }
        public void AddElement(SClientBindingModel model)
        {
            int maxId = 0;
            for (int i = 0; i < source.SClients.Count; ++i)
            {
                if (source.SClients[i].Id > maxId)
                {
                    maxId = source.SClients[i].Id;
                }
                if (source.SClients[i].SClientFIO == model.SClientFIO)
                {
                    throw new Exception("Уже есть клиент с таким ФИО");
                }
            }
            source.SClients.Add(new SClient
            {
                Id = maxId + 1,
                SClientFIO = model.SClientFIO
            });
        }
        public void UpdElement(SClientBindingModel model)
        {
            int index = -1;
            for (int i = 0; i < source.SClients.Count; ++i)
            {
                if (source.SClients[i].Id == model.Id)
                {
                    index = i;
                }
                if (source.SClients[i].SClientFIO == model.SClientFIO &&
                source.SClients[i].Id != model.Id)
                {
                    throw new Exception("Уже есть клиент с таким ФИО");
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            source.SClients[index].SClientFIO = model.SClientFIO;
        }
        public void DelElement(int id)
        {
            for (int i = 0; i < source.SClients.Count; ++i)

            {
                if (source.SClients[i].Id == id)
                {
                    source.SClients.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
    }
}
