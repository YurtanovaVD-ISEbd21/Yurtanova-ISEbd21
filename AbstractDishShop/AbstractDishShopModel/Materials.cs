using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractDishShopModel
{
    /// <summary>
    /// Материалы, требуемые для изготовления подарка
    /// </summary>
    public class Materials
    {
        public int Id { get; set; }
        public string MaterialsName { get; set; }
        [ForeignKey("MaterialsId")]
        public virtual List<DishMaterials> DishMaterials { get; set; }

        [ForeignKey("MaterialsId")]
        public virtual List<StockMaterials> StockMaterials { get; set; }
    }
}