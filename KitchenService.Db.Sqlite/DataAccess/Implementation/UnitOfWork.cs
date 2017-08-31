using KitchenService.Db.Sqlite;
using KitchenService.Schema;

namespace KitchenService.DataAccess.Implementation
{
    

    public class UnitOfWork : IUnitOfWork
    {
        private readonly KitchenContext _dbContext;
        private IAccountRepository _accountRepository;
        private ICategoryRepository _categoryRepository;
        private IRecipeRepository _recipeRepository;
        private IRecipeTypeRepository _recipeTypeRepository;
        private IListRepository _listRepository;
        private IPlanRepository _planRepository;

        public UnitOfWork(KitchenContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public IAccountRepository AccountRepository
        {
            get
            {
                if (this._accountRepository == null)
                {
                    this._accountRepository = new AccountRepository(this._dbContext);
                }
                return _accountRepository;
            }
        }

        public ICategoryRepository CategoryRepository
        {
            get
            {
                if (this._categoryRepository == null)
                {
                    this._categoryRepository = new CategoryRepository(this._dbContext);
                }
                return _categoryRepository;
            }
        }

        public IRecipeRepository RecipeRepository
        {
            get
            {
                if (this._recipeRepository == null)
                {
                    this._recipeRepository = new RecipeRepository(this._dbContext);
                }
                return _recipeRepository;
            }
        }

        public IRecipeTypeRepository RecipeTypeRepository
        {
            get
            {
                if (this._recipeTypeRepository == null)
                {
                    this._recipeTypeRepository = new RecipeTypeRepository(this._dbContext);
                }
                return _recipeTypeRepository;
            }
        }

        public IListRepository ListRepository
        {
            get
            {
                if (this._listRepository == null)
                {
                    this._listRepository = new ListRepository(this._dbContext);
                }
                return _listRepository;
            }
        }

        public IPlanRepository PlanRepository
        {
            get
            {
                if (this._planRepository == null)
                {
                    this._planRepository = new PlanRepository(this._dbContext);
                }
                return _planRepository;
            }
        }

        public void Save()
        {
            this._dbContext.SaveChanges();
        }

        public void Dispose()
        {
            _dbContext?.Dispose();
        }
    }
}
