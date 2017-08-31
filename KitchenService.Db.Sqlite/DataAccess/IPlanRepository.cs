using System;
using System.Collections.Generic;
using KitchenService.Schema;

namespace KitchenService.DataAccess
{
    public interface IPlanRepository : IRepository<DayPlan>
    {
        IEnumerable<DayPlan> GetOpen(long accountId);
        IEnumerable<DayPlan> GetOpen(long accountId, DateTime start, DateTime end);
        IEnumerable<DayPlan> GetClosed(long accountId, int page, int pageSize);
        DayPlan Get(long id);
        DayPlan Find(long accountId, DateTimeOffset date);
        void UpdateStock(DayPlanRecipe item);
        void Delete(List<DayPlanRecipe> value);
        void Hydrate(IEnumerable<DayPlan> plans, bool includeRecipeInfo = false);
        void Hydrate(DayPlan plan, bool includeRecipeInfo = false);
    }
}
