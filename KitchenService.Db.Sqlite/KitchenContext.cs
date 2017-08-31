using KitchenService.Schema;
using Microsoft.EntityFrameworkCore;

namespace KitchenService.Db.Sqlite
{
    public class KitchenContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<RecipeItem> RecipeItems { get; set; }
        public DbSet<RecipeStep> RecipeSteps { get; set; }
        public DbSet<ShoppingList> ShoppingLists { get; set; }
        public DbSet<ShoppingListItem> ShoppingListItems { get; set; }
        public DbSet<ListRecipe> ListRecipes { get; set; }
        public DbSet<DayPlan> DayPlans { get; set; }
        public DbSet<DayPlanRecipe> DayPlanRecipes { get; set; }
        public DbSet<RecipeType> RecipeTypes { get; set; }

        private readonly string _connectionString;
        public KitchenContext(string connectionString)
        {
            this._connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=" + this._connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DayPlan>()
                .ToTable("DayPlan");

            modelBuilder.Entity<DayPlanRecipe>()
                .ToTable("DayPlanRecipe");

            modelBuilder.Entity<RecipeType>()
                .ToTable("RecipeType");

            modelBuilder.Entity<DayPlanRecipe>()
                .HasKey(t => new { t.RecipeId, t.DayPlanId });

            modelBuilder.Entity<DayPlanRecipe>()
                .HasOne(pt => pt.Recipe)
                .WithMany(p => p.DayPlanRecipes)
                .HasForeignKey(pt => pt.RecipeId);

            modelBuilder.Entity<DayPlanRecipe>()
                .HasOne(pt => pt.DayPlan)
                .WithMany(t => t.DayPlanRecipes)
                .HasForeignKey(pt => pt.DayPlanId);

            modelBuilder.Entity<ListRecipe>()
                .HasKey(t => new { t.RecipeId, t.ShoppingListId });

            modelBuilder.Entity<ListRecipe>()
                .HasOne(pt => pt.Recipe)
                .WithMany(p => p.ListRecipes)
                .HasForeignKey(pt => pt.RecipeId);

            modelBuilder.Entity<ListRecipe>()
                .HasOne(pt => pt.ShoppingList)
                .WithMany(t => t.ListRecipes)
                .HasForeignKey(pt => pt.ShoppingListId);
        }
    }
}
