using System;

namespace KitchenService.DataAccess
{
    public interface IUnitOfWork : IDisposable
    {
        IAccountRepository AccountRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        IRecipeRepository RecipeRepository { get; }
        IRecipeTypeRepository RecipeTypeRepository { get; }
        IListRepository ListRepository { get; }
        IPlanRepository PlanRepository { get; }
        void Save();
    }
}
