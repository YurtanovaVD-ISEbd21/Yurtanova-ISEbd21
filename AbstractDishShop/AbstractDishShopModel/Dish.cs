using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractDishShopModel
{
    /// <summary>
    /// Блюдо, изготавливаемый в магазине
    /// </summary>
    public class Dish
    {
        public int Id { get; set; }
        public string DishName { get; set; }
        public decimal Price { get; set; }
        [ForeignKey("DishId")]
        public virtual List<SOrder> SOrders { get; set; }

        [ForeignKey("DishId")]
        public virtual List<DishMaterials> DishMaterialss { get; set; }
    }
}