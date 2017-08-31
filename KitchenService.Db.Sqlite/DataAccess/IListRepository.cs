using System.Collections.Generic;
using KitchenService.Schema;

namespace KitchenService.DataAccess
{
    public interface IListRepository : IRepository<ShoppingList>
    {
        ShoppingList GetOpen(long accountId);
        ShoppingList Get(long id);
        IEnumerable<ShoppingList> GetClosed(long accountId, int page, int pageSize);
        void Hydrate(ShoppingList list);
        void UpdateStock(ShoppingListItem item, bool isDone);
        void Update(ShoppingListItem value);
        void Delete(ShoppingListItem value);
        void Delete(IEnumerable<ShoppingListItem> value);
    }
}
