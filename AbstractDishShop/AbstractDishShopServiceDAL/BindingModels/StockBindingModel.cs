using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace AbstractDishShopServiceDAL.BindingModels
{
    [DataContract]
    public class StockBindingModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string StockName { get; set; }
    }
}
