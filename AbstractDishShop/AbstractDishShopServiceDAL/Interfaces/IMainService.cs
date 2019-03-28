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
        void CreateOrder(SOrderBindingModel model);
        void TakeOrderInWork(SOrderBindingModel model);
        void FinishOrder(SOrderBindingModel model);
        void PayOrder(SOrderBindingModel model);
    }
}
