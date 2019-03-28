using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractDishShopServiceDAL.BindingModels
{
    public class DishBindingModel
    {
        public int Id { get; set; }
        public string DishName { get; set; }
        public decimal Price { get; set; }
        public List<DishMaterialsBindingModel> DishMaterialss { get; set; }
    }
}
