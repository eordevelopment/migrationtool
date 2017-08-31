using System.ComponentModel.DataAnnotations.Schema;

namespace KitchenService.Schema
{
    public class ListRecipe
    {
        public long ShoppingListId { get; set; }
        [ForeignKey("ShoppingListId")]
        public ShoppingList ShoppingList { get; set; }

        public long RecipeId { get; set; }
        [ForeignKey("RecipeId")]
        public Recipe Recipe { get; set; }

        public bool IsDone { get; set; }
    }
}
