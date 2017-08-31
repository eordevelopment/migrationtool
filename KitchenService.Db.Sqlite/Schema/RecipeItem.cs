using System.Runtime.Serialization;

namespace KitchenService.Schema
{
    [DataContract]
    public class RecipeItem
    {
        [DataMember]
        public long Id { get; set; }

        [DataMember]
        public float Amount { get; set; }

        [DataMember]
        public string Instructions { get; set; }

        [DataMember]
        public Item Item { get; set; }
    }
}
