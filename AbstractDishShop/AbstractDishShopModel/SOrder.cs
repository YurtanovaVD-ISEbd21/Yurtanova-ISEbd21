using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractDishShopModel
{/// <summary>
 /// Заказ клиента
 /// </summary>
    public class SOrder
    {
        public int Id { get; set; }
        public int SClientId { get; set; }
        public int DishId { get; set; }
        public int? ImplementerId { get; set; }
        public string ImplementerName { get; set; }
        public int Count { get; set; }
        public decimal Sum { get; set; }
        public SOrderStatus Status { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime? DateImplement { get; set; }
        public virtual SClient SClient { get; set; }
        public virtual Dish Dish { get; set; }
        public virtual Implementer Implementer { get; set; }
    }
}
    
