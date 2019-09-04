using AbstractDishShopServiceDAL.BindingModels;
using AbstractDishShopServiceDAL.ViewModel;
using System.Collections.Generic;

namespace AbstractDishShopServiceDAL.Interfaces
{
    public interface IMainService
    {
        List<SOrderViewModel> GetList();
        List<SOrderViewModel> GetFreeSOrders();
        void CreateSOrder(SOrderBindingModel model);
        void TakeSOrderInWork(SOrderBindingModel model);
        void FinishSOrder(SOrderBindingModel model);
        void PaySOrder(SOrderBindingModel model);
        void PutMaterialsOnStock(StockMaterialsBindingModel model);
        

    }
}

