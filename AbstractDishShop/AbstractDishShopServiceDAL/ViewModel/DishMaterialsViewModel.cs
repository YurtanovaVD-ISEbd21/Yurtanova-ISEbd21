using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractDishShopServiceDAL.ViewModel
{
    public class DishMaterialsViewModel
    {
        public int Id { get; set; }
        public int DishId { get; set; }
        public int MaterialsId { get; set; }
        [DisplayName("Материал")]
        public string MaterialsName { get; set; }
        [DisplayName("Количество")]
        public int Count { get; set; }
    }
}
