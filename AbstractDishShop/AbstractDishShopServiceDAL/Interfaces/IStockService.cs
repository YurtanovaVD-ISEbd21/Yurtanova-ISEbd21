using AbstractDishShopServiceDAL.BindingModels;
using AbstractDishShopServiceDAL.ViewModel;
using System.Collections.Generic;


namespace AbstractDishShopServiceDAL.Interfaces
{
    public interface IStockService
    {
        List<StockViewModel> GetList();
        StockViewModel GetElement(int id);
        void AddElement(StockBindingModel model);
        void UpdElement(StockBindingModel model);
        void DelElement(int id);
    }
}
