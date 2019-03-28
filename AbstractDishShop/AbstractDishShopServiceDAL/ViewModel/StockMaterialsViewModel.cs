using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractDishShopServiceDAL.ViewModel
{
    public class StockMaterialsViewModel
    {
        public int Id { get; set; }

public int StockId { get; set; }
        public int MaterialsId { get; set; }
        [DisplayName("Название компонента")]
        public string MaterialsName { get; set; }
        [DisplayName("Количество")]
        public int Count { get; set; }
    }
}