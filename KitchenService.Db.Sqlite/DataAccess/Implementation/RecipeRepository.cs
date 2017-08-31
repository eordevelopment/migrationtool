using System;
using System.Collections.Generic;
using System.Linq;
using KitchenService.Db.Sqlite;
using KitchenService.Schema;

namespace KitchenService.DataAccess.Implementation
{
    public class RecipeRepository : Repository<Recipe>, IRecipeRepository
    {
        public RecipeRepository(KitchenContext dbContext) :base(dbContext)
        {
        }

        public IEnumerable<Recipe> GetAll(long accountId)
        {
            return this.DbSet.Where(x => x.AccountId == accountId);
        }

        public Recipe Get(long id)
        {
            return this.DbSet.FirstOrDefault(x => x.Id == id);
        }

        public Recipe Get(string key)
        {
            return this.DbSet.FirstOrDefault(x => x.Key == key);
        }

        public Recipe Find(long accountId, string name)
        {
            return this.DbSet.FirstOrDefault(x => x.AccountId == accountId && x.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public IEnumerable<RecipeItem> GetItems(List<long> itemIds)
        {
            return this.DbContext.RecipeItems.Where(x => itemIds.Contains(x.Item.Id));
        }

        public void Hydrate(IEnumerable<Recipe> recipes)
        {
            if (recipes != null)
            {
                foreach (var recipe in recipes)
                {
                    this.DbContext.Entry(recipe).Reference(x => x.RecipeType).Load();
                }
            }
        }

        public void Hydrate(Recipe recipe, bool includePlan = false)
        {
            if (recipe != null)
            {
                this.DbContext.Entry(recipe).Collection(b => b.RecipeItems).Load();
                this.DbContext.Entry(recipe).Collection(b => b.RecipeSteps).Load();
                this.DbContext.Entry(recipe).Reference(x => x.RecipeType).Load();
                foreach (var recipeItem in recipe.RecipeItems)
                {
                    this.DbContext.Entry(recipeItem).Reference(x => x.Item).Load();
                }

                if (includePlan)
                {
                    this.DbContext.Entry(recipe).Collection(b => b.DayPlanRecipes).Load();
                    foreach (var dayPlanRecipe in recipe.DayPlanRecipes)
                    {
                        this.DbContext.Entry(dayPlanRecipe).Reference(x => x.DayPlan).Load();
                    }
                }
            }
        }

        public void Insert(RecipeItem value)
        {
            this.DbContext.RecipeItems.Add(value);
        }

        public void Update(RecipeItem value)
        {
            this.DbContext.RecipeItems.Update(value);
        }

        public void Delete(RecipeItem value)
        {
            this.DbContext.RecipeItems.Remove(value);
        }

        public void Delete(IEnumerable<RecipeItem> value)
        {
            this.DbContext.RecipeItems.RemoveRange(value);
        }

        public void Insert(RecipeStep value)
        {
            this.DbContext.RecipeSteps.Add(value);
        }

        public void Update(RecipeStep value)
        {
            this.DbContext.RecipeSteps.Update(value);
        }

        public void Delete(RecipeStep value)
        {
            this.DbContext.RecipeSteps.Remove(value);
        }

        public void Delete(IEnumerable<RecipeStep> value)
        {
            this.DbContext.RecipeSteps.RemoveRange(value);
        }
    }
}
