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
    public class StockMaterialsViewModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int StockId { get; set; }
        [DataMember]
        public int MaterialsId { get; set; }
        [DataMember]
        [DisplayName("Название компонента")]
        public string MaterialsName { get; set; }
        [DataMember]
        [DisplayName("Количество")]
        public int Count { get; set; }
    }
}