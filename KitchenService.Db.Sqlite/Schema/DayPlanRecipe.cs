using System.ComponentModel.DataAnnotations.Schema;

namespace KitchenService.Schema
{
    public class DayPlanRecipe
    {
        public long DayPlanId { get; set; }
        [ForeignKey("DayPlanId")]
        public DayPlan DayPlan { get; set; }

        public long RecipeId { get; set; }
        [ForeignKey("RecipeId")]
        public Recipe Recipe { get; set; }

        public bool IsDone { get; set; }
    }
}
