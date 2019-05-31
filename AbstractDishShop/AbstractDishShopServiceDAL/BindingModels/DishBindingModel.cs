using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace AbstractDishShopServiceDAL.BindingModels
{
    [DataContract]
    public class DishBindingModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string DishName { get; set; }
        [DataMember]
        public decimal Price { get; set; }
        [DataMember]
        public List<DishMaterialsBindingModel> DishMaterialss { get; set; }
    }
}
