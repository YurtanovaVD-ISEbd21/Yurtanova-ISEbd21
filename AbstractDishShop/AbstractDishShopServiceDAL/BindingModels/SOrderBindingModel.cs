using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractDishShopServiceDAL.BindingModels
{
    public class SOrderBindingModel
    {
        public int Id { get; set; }
        public int SClientId { get; set; }
        public int DishId { get; set; }
        public int Count { get; set; }
        public decimal Sum { get; set; }
    }
}
