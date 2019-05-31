using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace AbstractDishShopServiceDAL.BindingModels
{
    [DataContract]
    public class SClientSOrdersModel
    {
        [DataMember]
        public string SClientName { get; set; }
        [DataMember]
        public string DateCreate { get; set; }
        [DataMember]
        public string DishName { get; set; }
        [DataMember]
        public int Count { get; set; }
        [DataMember]
        public decimal Sum { get; set; }
        [DataMember]
        public string Status { get; set; }
    }
}
