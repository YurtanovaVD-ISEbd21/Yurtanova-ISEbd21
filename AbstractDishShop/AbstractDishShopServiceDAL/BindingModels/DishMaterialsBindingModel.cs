using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractDishShopServiceDAL.BindingModels
{
    public class DishMaterialsBindingModel
    {
        public int Id { get; set; }
        public int DishId { get; set; }
        public int MaterialsId { get; set; }
        public int Count { get; set; }
    }
}
