using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractDishShopServiceDAL.ViewModel
{
    public class MaterialsViewModel
    {
        public int Id { get; set; }
        [DisplayName("Название материала")]
        public string MaterialsName { get; set; }
    }
}
