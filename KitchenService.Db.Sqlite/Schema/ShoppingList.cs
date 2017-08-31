using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace KitchenService.Schema
{
    public class ShoppingList
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public bool IsDone { get; set; }

        public DateTimeOffset CreatedOn { get; set; }

        public List<ShoppingListItem> Items { get; set; }

        public List<ListRecipe> ListRecipes { get; set; }

        public long AccountId { get; set; }
        [ForeignKey("AccountId")]
        public Account Account { get; set; }
    }
}
