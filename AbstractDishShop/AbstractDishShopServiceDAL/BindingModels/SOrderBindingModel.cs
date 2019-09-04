using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace AbstractDishShopServiceDAL.BindingModels
{
    [DataContract]
    public class SOrderBindingModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int SClientId { get; set; }
        [DataMember]
        public int DishId { get; set; }
        [DataMember]
        public int? ImplementerId { get; set; }
        [DataMember]
        public int Count { get; set; }
        [DataMember]
        public decimal Sum { get; set; }
    }
}
