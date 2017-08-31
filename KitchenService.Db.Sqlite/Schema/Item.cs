using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace KitchenService.Schema
{
    [DataContract]
    public class Item
    {
        [DataMember]
        public long Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public float Quantity { get; set; }

        [DataMember]
        public string UnitType { get; set; }

        public long AccountId { get; set; }
        [ForeignKey("AccountId")]
        public Account Account { get; set; }
    }
}
