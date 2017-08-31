using KitchenService.Schema;

namespace KitchenService.DataAccess
{
    public interface IAccountRepository : IRepository<Account>
    {
        Account Find(string token);
        Account Get(string userName);
        Account Get(string userName, string password);
    }
}
