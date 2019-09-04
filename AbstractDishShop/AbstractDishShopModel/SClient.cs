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
    /// Клиент магазина
    /// </summary>
    public class SClient
    {
        public int Id { get; set; }
        [Required]
        public string SClientFIO { get; set; }
        public string Mail { get; set; }
        [ForeignKey("SClientId")]
        public virtual List<SOrder> SOrders { get; set; }
        [ForeignKey("ClientId")]
        public virtual List<MessageInfo> MessageInfos { get; set; }
    }
}

