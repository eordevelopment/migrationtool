using System;
using System.Collections.Generic;
using System.Linq;
using KitchenService.Db.Sqlite;
using KitchenService.Schema;

namespace KitchenService.DataAccess.Implementation
{
    public class RecipeTypeRepository : Repository<RecipeType>, IRecipeTypeRepository
    {
        public RecipeTypeRepository(KitchenContext dbContext) : base(dbContext)
        {
        }

        public IEnumerable<RecipeType> GetAll(long accountId)
        {
            return this.DbSet.Where(x => x.AccountId == accountId);
        }

        public RecipeType Get(long id)
        {
            return this.DbSet.FirstOrDefault(x => x.Id == id);
        }

        public RecipeType Find(long accountId, string name)
        {
            return this.DbSet.FirstOrDefault(x => x.AccountId == accountId && x.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }
    }
}
