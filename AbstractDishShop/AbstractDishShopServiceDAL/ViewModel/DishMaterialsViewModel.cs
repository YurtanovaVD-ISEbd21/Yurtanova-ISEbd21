using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace AbstractDishShopServiceDAL.ViewModel
{
    [DataContract]
    public class DishMaterialsViewModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int DishId { get; set; }
        [DataMember]
        public int MaterialsId { get; set; }
        [DataMember]
        [DisplayName("Материал")]
        public string MaterialsName { get; set; }
        [DataMember]
        [DisplayName("Количество")]
        public int Count { get; set; }
    }
}
