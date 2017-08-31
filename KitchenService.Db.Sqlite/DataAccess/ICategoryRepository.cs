using System.Collections.Generic;
using KitchenService.Schema;

namespace KitchenService.DataAccess
{
    public interface ICategoryRepository : IRepository<Category>
    {
        IEnumerable<Category> GetAll(long accountId);
        Category Get(long id);
        Category Find(long accountId, string name);
        Item FindItem(long accountId, long itemId, string name);
        Item FindItem(long accountId, string name);
        IEnumerable<Item> SearchItems(long accountId, string name, int max);

        void Insert(Item value);
        void Update(Item value);
        void Delete(IEnumerable<Item> value);

        void Hydrate(Category category);
    }
}
