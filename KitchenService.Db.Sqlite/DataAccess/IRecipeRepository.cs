using System.Collections.Generic;
using KitchenService.Schema;

namespace KitchenService.DataAccess
{
    public interface IRecipeRepository : IRepository<Recipe>
    {
        IEnumerable<Recipe> GetAll(long accountId);
        Recipe Get(long id);
        Recipe Get(string key);
        Recipe Find(long accountId, string name);
        IEnumerable<RecipeItem> GetItems(List<long> itemIds);
        void Hydrate(IEnumerable<Recipe> recipes);
        void Hydrate(Recipe recipe, bool includePlan = false);
        void Insert(RecipeItem value);
        void Update(RecipeItem value);
        void Delete(RecipeItem value);
        void Delete(IEnumerable<RecipeItem> value);
        void Insert(RecipeStep value);
        void Update(RecipeStep value);
        void Delete(RecipeStep value);
        void Delete(IEnumerable<RecipeStep> value);
    }
}
