using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace KitchenService.Schema
{
    [DataContract]
    public class Recipe
    {
        [DataMember]
        public long Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        public long TypeId { get; set; }

        [DataMember]
        [ForeignKey("TypeId")]
        public RecipeType RecipeType { get; set; }

        [DataMember]
        public string Key { get; set; }

        [DataMember]
        public List<RecipeStep> RecipeSteps { get; set; }
        [DataMember]
        public List<RecipeItem> RecipeItems { get; set; }

        public long AccountId { get; set; }
        [ForeignKey("AccountId")]
        public Account Account { get; set; }

        public List<DayPlanRecipe> DayPlanRecipes { get; set; }

        public List<ListRecipe> ListRecipes { get; set; }
    }
}
