using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractDishShopModel
{
    public class StockMaterials
    {
        public int Id { get; set; }
        public int StockId { get; set; }
        public int MaterialsId { get; set; }
        public int Count { get; set; }
        public virtual Stock Stock { get; set; }
        public virtual Materials Materials { get; set; }
    }
}