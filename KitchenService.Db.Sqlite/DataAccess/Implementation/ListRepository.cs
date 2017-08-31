using System.Collections.Generic;
using System.Linq;
using KitchenService.Db.Sqlite;
using KitchenService.Schema;
using Microsoft.EntityFrameworkCore;

namespace KitchenService.DataAccess.Implementation
{
    public class ListRepository : Repository<ShoppingList>, IListRepository
    {
        public ListRepository(KitchenContext dbContext) :base(dbContext)
        {
        }

        public ShoppingList GetOpen(long accountId)
        {
            return this.DbSet.FirstOrDefault(x => x.AccountId == accountId && !x.IsDone);
        }

        public ShoppingList Get(long id)
        {
            return this.DbSet.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<ShoppingList> GetClosed(long accountId, int page, int pageSize)
        {
            return this.DbSet
                .Where(x => x.AccountId == accountId && x.IsDone)
                .OrderByDescending(x => x.CreatedOn)
                .Skip(pageSize * page)
                .Take(pageSize);
        }

        public void Hydrate(ShoppingList list)
        {
            this.DbContext.Entry(list).Collection(b => b.Items).Load();
            this.DbContext.Entry(list).Collection(b => b.ListRecipes).Load();
            foreach (var item in list.Items)
            {
                this.DbContext.Entry(item).Reference(x => x.Item).Load();
            }

            foreach (var listRecipe in list.ListRecipes)
            {
                this.DbContext.Entry(listRecipe).Reference(x => x.Recipe).Load();

                this.DbContext.Entry(listRecipe.Recipe).Collection(b => b.RecipeItems).Load();
                this.DbContext.Entry(listRecipe.Recipe).Collection(b => b.RecipeSteps).Load();
                this.DbContext.Entry(listRecipe.Recipe).Reference(x => x.RecipeType).Load();
                foreach (var recipeItem in listRecipe.Recipe.RecipeItems)
                {
                    this.DbContext.Entry(recipeItem).Reference(x => x.Item).Load();
                }
            }
        }

        public void UpdateStock(ShoppingListItem shoppingListItem, bool isDone)
        {
            var item = shoppingListItem.Item;
            if (isDone) item.Quantity += shoppingListItem.Amount;
            else item.Quantity -= shoppingListItem.Amount;

            this.DbContext.Items.Update(item);
        }

        public void Update(ShoppingListItem value)
        {
            this.DbContext.ShoppingListItems.Attach(value);
            this.DbContext.Entry(value).State = EntityState.Modified;
        }

        public void Delete(ShoppingListItem value)
        {
            this.DbContext.ShoppingListItems.Remove(value);
        }

        public void Delete(IEnumerable<ShoppingListItem> value)
        {
            this.DbContext.ShoppingListItems.RemoveRange(value);
        }
    }
}
