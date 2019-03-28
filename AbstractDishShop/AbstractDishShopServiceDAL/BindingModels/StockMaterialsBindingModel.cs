using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractDishShopServiceDAL.BindingModels
{
    public class StockMaterialsBindingModel
    {
        public int Id { get; set; }
        public int StockId { get; set; }
        public int MaterialsId { get; set; }
        public int Count { get; set; }
    }
}
