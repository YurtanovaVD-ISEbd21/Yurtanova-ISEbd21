using AbstractDishShopServiceDAL.BindingModels;
using AbstractDishShopServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractDishShopServiceDAL.Interfaces
{
    public interface IImplementerService
    {
        List<ImplementerViewModel> GetList();
        ImplementerViewModel GetElement(int id);
        void AddElement(ImplementerBindingModel model);
        void UpdElement(ImplementerBindingModel model);
        void DelElement(int id);
        ImplementerViewModel GetFreeImplementer();
       
    }
}
