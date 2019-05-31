using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace AbstractDishShopServiceDAL.BindingModels
{
    [DataContract]
    public class SClientBindingModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string SClientFIO { get; set; }
    }
}
