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
    public class SClientViewModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        [DisplayName("ФИО Клиента")]
        public string SClientFIO { get; set; }
    }
}
