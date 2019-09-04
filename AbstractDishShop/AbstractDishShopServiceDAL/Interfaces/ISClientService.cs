using AbstractDishShopServiceDAL.Attributies;
using AbstractDishShopServiceDAL.BindingModels;
using AbstractDishShopServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractDishShopServiceDAL.Interfaces
{
    [CustomInterface("Интерфейс для работы с клиентами")]
    public interface IClientService
    {
        [CustomMethod("Метод получения списка клиентов")]
        List<SClientViewModel> GetList();
        [CustomMethod("Метод получения клиента по id")]
        SClientViewModel GetElement(int id);
        [CustomMethod("Метод добавления клиента")]
        void AddElement(SClientBindingModel model);
        [CustomMethod("Метод изменения данных по клиенту")]
        void UpdElement(SClientBindingModel model);
        [CustomMethod("Метод удаления клиента")]
        void DelElement(int id);
    }
}
