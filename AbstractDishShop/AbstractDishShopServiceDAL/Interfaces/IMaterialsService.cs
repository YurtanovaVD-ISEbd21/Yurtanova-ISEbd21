using AbstractDishShopServiceDAL.BindingModels;
using AbstractDishShopServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractDishShopServiceDAL.Interfaces
{
    public interface IMaterialsService
    {
        List<MaterialsViewModel> GetList();
        MaterialsViewModel GetElement(int id);
        void AddElement(MaterialsBindingModel model);
        void UpdElement(MaterialsBindingModel model);
        void DelElement(int id);
    }
}
