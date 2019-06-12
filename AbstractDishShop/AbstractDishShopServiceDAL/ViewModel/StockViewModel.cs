using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractDishShopServiceDAL.ViewModel
{
    public class StockViewModel
    {
        public int Id { get; set; }
        [DisplayName("Название склада")]
        public string StockName { get; set; }
        public List<StockMaterialsViewModel> StockMaterialss { get; set; }
    }
}
