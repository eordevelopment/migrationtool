using KitchenService.Db.Sqlite;
using KitchenService.Schema;
using Microsoft.EntityFrameworkCore;

namespace KitchenService.DataAccess.Implementation
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected KitchenContext DbContext;
        protected DbSet<TEntity> DbSet;

        public Repository(KitchenContext context)
        {
            this.DbContext = context;
            this.DbSet = context.Set<TEntity>();
        }

        public virtual void Insert(TEntity entity)
        {
            DbSet.Add(entity);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (this.DbContext.Entry(entityToDelete).State == EntityState.Detached)
            {
                DbSet.Attach(entityToDelete);
            }
            DbSet.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            DbSet.Attach(entityToUpdate);
            this.DbContext.Entry(entityToUpdate).State = EntityState.Modified;
        }
    }
}
