using System;
using System.Linq;
using KitchenService.Db.Sqlite;
using KitchenService.Schema;

namespace KitchenService.DataAccess.Implementation
{
    public class AccountRepository: Repository<Account>, IAccountRepository
    {
        public AccountRepository(KitchenContext dbContext) : base(dbContext)
        {
        }

        public Account Find(string token)
        {
            return this.DbSet.FirstOrDefault(x => x.Token == token);
        }

        public Account Get(string userName)
        {
            return this.DbSet.FirstOrDefault(x => x.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase)); 
        }

        public Account Get(string userName, string password)
        {
            return this.DbSet
                .FirstOrDefault(x =>
                    x.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase) &&
                    x.HashedPassword.Equals(password, StringComparison.OrdinalIgnoreCase));
        }
    }
}
