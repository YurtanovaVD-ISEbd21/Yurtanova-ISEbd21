using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractDishShopServiceDAL.ViewModel
{
    public class DishViewModel
    {
        public int Id { get; set; }
        [DisplayName("Название блюда")]
        public string DishName { get; set; }
        [DisplayName("Цена")]
        public decimal Price { get; set; }
        public List<DishMaterialsViewModel> DishMaterials { get; set; }
    }
}
