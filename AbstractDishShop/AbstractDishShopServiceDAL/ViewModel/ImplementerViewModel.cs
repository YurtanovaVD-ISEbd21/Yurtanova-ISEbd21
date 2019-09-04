using System.Runtime.Serialization;

namespace AbstractDishShopServiceDAL.ViewModel
{
    [DataContract]
    public class ImplementerViewModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string ImplementerName { get; set; }
    }
}
