using System.Collections.Generic;
using KitchenService.Schema;

namespace KitchenService.DataAccess
{
    public interface IRecipeTypeRepository : IRepository<RecipeType>
    {
        IEnumerable<RecipeType> GetAll(long accountId);
        RecipeType Get(long id);
        RecipeType Find(long accountId, string name);
    }
}
