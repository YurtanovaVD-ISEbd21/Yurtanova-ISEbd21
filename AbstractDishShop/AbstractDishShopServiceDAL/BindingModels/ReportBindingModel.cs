using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace AbstractDishShopServiceDAL.BindingModels
{
    [DataContract]
    public class ReportBindingModel
    {
        [DataMember]
        public string FileName { get; set; }
        [DataMember]
        public DateTime? DateFrom { get; set; }
        [DataMember]
        public DateTime? DateTo { get; set; }
    }
}
