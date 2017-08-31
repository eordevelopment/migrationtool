namespace KitchenService.DataAccess
{
    public interface IRepository<in TEntity> where TEntity : class
    {
        void Insert(TEntity entity);

        void Delete(TEntity entityToDelete);

        void Update(TEntity entityToUpdate);
    }
}
