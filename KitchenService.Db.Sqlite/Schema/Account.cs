using System.Collections.Generic;
using System.Runtime.Serialization;

namespace KitchenService.Schema
{
    [DataContract]
    public class Account
    {
        public long Id { get; set; }

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string HashedPassword { get; set; }

        [DataMember]
        public string Token { get; set; }

        public List<Category> Categories { get; set; }
        public List<Recipe> Recipies { get; set; }
        public List<ShoppingList> ShoppingLists { get; set; }
        public List<Item> Items { get; set; }
        public List<RecipeType> RecipeTypes { get; set; }
        public List<DayPlan> DayPlan { get; set; }
    }
}
