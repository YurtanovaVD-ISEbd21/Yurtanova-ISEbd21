using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace AbstractDishShopServiceDAL.BindingModels
{
    [DataContract]
    public class DishMaterialsBindingModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int DishId { get; set; }
        [DataMember]
        public int MaterialsId { get; set; }
        [DataMember]
        public int Count { get; set; }
    }
}
