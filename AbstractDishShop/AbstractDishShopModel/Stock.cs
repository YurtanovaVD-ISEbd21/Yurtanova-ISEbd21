using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractDishShopModel
{
    public class Stock
    {
        public int Id { get; set; }
        public string StockName { get; set; }
        [ForeignKey("StockId")]
        public virtual List<StockMaterials> StockMaterialss { get; set; }
    }
}
