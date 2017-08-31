using System.Runtime.Serialization;

namespace KitchenService.Schema
{
    [DataContract]
    public class RecipeStep
    {
        [DataMember]
        public long Id { get; set; }

        [DataMember]
        public int StepNumber { get; set; }

        [DataMember]
        public string Description { get; set; }
    }
}
