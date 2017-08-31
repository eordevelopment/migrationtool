using System;
using System.Collections.Generic;
using System.Linq;
using KitchenService.Db.Sqlite;
using KitchenService.Schema;

namespace KitchenService.DataAccess.Implementation
{
    public class PlanRepository : Repository<DayPlan>, IPlanRepository
    {
        public PlanRepository(KitchenContext dbContext) : base(dbContext)
        {
        }

        public IEnumerable<DayPlan> GetOpen(long accountId)
        {
            return this.DbSet.Where(x => x.AccountId == accountId && x.DayPlanRecipes.Any(y => !y.IsDone));
        }

        public IEnumerable<DayPlan> GetOpen(long accountId, DateTime start, DateTime end)
        {
            return this.DbSet.Where(x => x.AccountId == accountId && (x.DayPlanRecipes.Any(y => !y.IsDone) || (x.DateTime >= start && x.DateTime <= end)));
        }

        public IEnumerable<DayPlan> GetClosed(long accountId, int page, int pageSize)
        {
            return this.DbSet
                .Where(x => x.AccountId == accountId && x.DayPlanRecipes.All(y => y.IsDone))
                .OrderByDescending(x => x.DateTime)
                .Skip(pageSize * page)
                .Take(pageSize);
        }

        public DayPlan Get(long id)
        {
            return this.DbSet.FirstOrDefault(x => x.Id == id);
        }

        public DayPlan Find(long accountId, DateTimeOffset date)
        {
            return this.DbSet.FirstOrDefault(x => x.AccountId == accountId && x.DateTime == date);
        }

        public void UpdateStock(DayPlanRecipe item)
        {
            this.DbContext.Entry(item.Recipe).Collection(b => b.RecipeItems).Load();
            foreach (var recipeItem in item.Recipe.RecipeItems)
            {
                this.DbContext.Entry(recipeItem).Reference(x => x.Item).Load();
                recipeItem.Item.Quantity -= recipeItem.Amount;
                if (recipeItem.Item.Quantity < 0) recipeItem.Item.Quantity = 0;
                this.DbContext.Items.Update(recipeItem.Item);
            }
        }

        public void Delete(List<DayPlanRecipe> value)
        {
            this.DbContext.DayPlanRecipes.RemoveRange(value);
        }

        public void Hydrate(IEnumerable<DayPlan> plans, bool includeRecipeInfo = false)
        {
            foreach (var dayPlan in plans)
            {
                this.Hydrate(dayPlan, includeRecipeInfo);
            }
        }

        public void Hydrate(DayPlan plan, bool includeRecipeInfo = false)
        {
            this.DbContext.Entry(plan).Collection(b => b.DayPlanRecipes).Load();
            foreach (var planItem in plan.DayPlanRecipes)
            {
                this.DbContext.Entry(planItem).Reference(x => x.Recipe).Load();
                if (includeRecipeInfo)
                {
                    if (planItem.Recipe != null)
                    {
                        this.DbContext.Entry(planItem.Recipe).Collection(b => b.RecipeItems).Load();
                        this.DbContext.Entry(planItem.Recipe).Collection(b => b.RecipeSteps).Load();
                        this.DbContext.Entry(planItem.Recipe).Reference(x => x.RecipeType).Load();
                        foreach (var recipeItem in planItem.Recipe.RecipeItems)
                        {
                            this.DbContext.Entry(recipeItem).Reference(x => x.Item).Load();
                        }
                    }
                }
            }
        }
    }
}
