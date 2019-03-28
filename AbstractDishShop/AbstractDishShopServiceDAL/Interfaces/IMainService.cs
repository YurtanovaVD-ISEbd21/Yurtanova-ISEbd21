using AbstractDishShopServiceDAL.BindingModels;
using AbstractDishShopServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractDishShopServiceDAL.Interfaces
{
    public interface IMainService
    {
        List<SOrderViewModel> GetList();
        void CreateSOrder(SOrderBindingModel model);
        void TakeSOrderInWork(SOrderBindingModel model);
        void FinishSOrder(SOrderBindingModel model);
        void PaySOrder(SOrderBindingModel model);
        void PutMaterialsOnStock(StockMaterialsBindingModel model);
        

    }
}

