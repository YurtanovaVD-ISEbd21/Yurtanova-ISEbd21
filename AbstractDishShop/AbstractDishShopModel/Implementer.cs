using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractDishShopModel
{
    /// <summary>
    /// Исполнитель, выполняющий заказы клиентов
    /// </summary>
    public class Implementer
    {
        public int Id { get; set; }
        [Required]
        public string ImplementerName { get; set; }
        [ForeignKey("ImplementerId")]
        public virtual List<SOrder> SOrders { get; set; }
    }
}