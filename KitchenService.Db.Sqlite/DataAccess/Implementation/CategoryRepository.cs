using System;
using System.Collections.Generic;
using System.Linq;
using KitchenService.Db.Sqlite;
using KitchenService.Schema;

namespace KitchenService.DataAccess.Implementation
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(KitchenContext dbContext) : base(dbContext)
        {
        }

        public IEnumerable<Category> GetAll(long accountId)
        {
            return this.DbSet.Where(x => x.AccountId == accountId);
        }

        public Category Get(long id)
        {
            return this.DbSet.FirstOrDefault(x => x.Id == id);
        }

        public Category Find(long accountId, string name)
        {
            return this.DbSet.FirstOrDefault(x => x.AccountId == accountId && x.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public Item FindItem(long accountId, long itemId, string name)
        {
            return this.DbContext.Items.FirstOrDefault(x => x.Id == itemId) ??
                   this.DbContext.Items.FirstOrDefault(x => x.AccountId == accountId && x.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public Item FindItem(long accountId, string name)
        {
            return this.DbContext.Items.FirstOrDefault(x => x.AccountId == accountId && x.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public IEnumerable<Item> SearchItems(long accountId, string name, int max)
        {
            return this.DbContext.Items.Where(x => x.AccountId == accountId && x.Name.StartsWith(name, StringComparison.OrdinalIgnoreCase)).Take(max);
        }

        public void Insert(Item value)
        {
            this.DbContext.Items.Add(value);
        }

        public void Update(Item value)
        {
            this.DbContext.Items.Update(value);
        }

        public void Delete(IEnumerable<Item> value)
        {
            this.DbContext.Items.RemoveRange(value);
        }

        public void Hydrate(Category category)
        {
            if (category != null)
            {
                this.DbContext.Entry(category).Collection(b => b.Items).Load();
            }
        }
    }
}
