using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using KitchenService.Db.Sqlite;
using KitchenServiceV2.Db.Mongo;
using KitchenServiceV2.Db.Mongo.Repository;
using KitchenServiceV2.Db.Mongo.Schema;
using Microsoft.Extensions.Configuration;

namespace MigrationTool
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            var configuration = builder.Build();

            //var sqlConnection = new KitchenContext(configuration.GetSection("sqliteConnection").Value);
            var mongoConnection = new DbContext(configuration.GetSection("mongoDbConnection").Value, configuration.GetSection("mongoDb").Value);

            //repos
            //var accountRepo = new AccountRepository(mongoConnection);
            //var itemRepo = new ItemRepository(mongoConnection);
            //var recipeTypeRepo = new RecipeTypeRepository(mongoConnection);
            var recipeRepo = new RecipeRepository(mongoConnection);
            //var planRepo = new PlanRepository(mongoConnection);

            //mongoConnection.Db.DropCollection(accountRepo.CollectionName);
            //mongoConnection.Db.DropCollection(itemRepo.CollectionName);
            //mongoConnection.Db.DropCollection(recipeTypeRepo.CollectionName);
            //mongoConnection.Db.DropCollection(recipeRepo.CollectionName);
            //mongoConnection.Db.DropCollection(planRepo.CollectionName);

            //foreach (var account in sqlConnection.Accounts)
            //{
            //    sqlConnection.Entry(account).Collection(b => b.Items).Load();
            //    sqlConnection.Entry(account).Collection(b => b.RecipeTypes).Load();
            //    sqlConnection.Entry(account).Collection(b => b.Recipies).Load();

            //    //Migrate account
            //    var accountDb = new Account
            //    {
            //       UserName = account.UserName,
            //       HashedPassword = account.HashedPassword,
            //       UserToken = account.Token
            //    };

            //    accountRepo.Upsert(accountDb).Wait();

            //    //Migrate items
            //    var items = account.Items.Select(item => new Item
            //    {
            //        Name = item.Name.ToLowerInvariant(),
            //        Quantity = 0,
            //        UnitType = item.UnitType,
            //        UserToken = accountDb.UserToken
            //    }).ToList();
            //    itemRepo.Upsert(items).Wait();
            //    var itemsByName = items.ToDictionary(x => x.Name);

            //    //Migrate recipe types
            //    var recipeTypes = account.RecipeTypes.Select(x => new RecipeType
            //    {
            //        Name = x.Name.ToLowerInvariant(),
            //        UserToken = accountDb.UserToken
            //    }).ToList();
            //    recipeTypeRepo.Upsert(recipeTypes).Wait();
            //    var recipeTypesByName = recipeTypes.ToDictionary(x => x.Name);

            //    //Migrate Recipes
            //    var recipes = new List<Recipe>();
            //    foreach (var recipe in account.Recipies)
            //    {
            //        sqlConnection.Entry(recipe).Collection(b => b.RecipeItems).Load();
            //        sqlConnection.Entry(recipe).Collection(b => b.RecipeSteps).Load();
            //        sqlConnection.Entry(recipe).Reference(x => x.RecipeType).Load();
            //        foreach (var recipeItem in recipe.RecipeItems)
            //        {
            //            sqlConnection.Entry(recipeItem).Reference(x => x.Item).Load();
            //        }

            //        var recipeDb = new Recipe
            //        {
            //            UserToken = accountDb.UserToken,
            //            Name = recipe.Name.ToLowerInvariant(),
            //            Key = recipe.Name,
            //            RecipeTypeId = recipeTypesByName[recipe.RecipeType.Name.ToLowerInvariant()].Id,
            //            RecipeSteps = recipe.RecipeSteps.Select(x => new RecipeStep
            //            {
            //                Description = x.Description,
            //                StepNumber = x.StepNumber
            //            }).ToList(),
            //            RecipeItems = recipe.RecipeItems.Select(x => new RecipeItem
            //            {
            //                Amount = x.Amount,
            //                Instructions = x.Instructions,
            //                ItemId = itemsByName[x.Item.Name.ToLowerInvariant()].Id
            //            }).ToList()
            //        };

            //        recipes.Add(recipeDb);
            //    }
            //    recipeRepo.Upsert(recipes).Wait();
            //}

            var recipes = recipeRepo.GetAll().Result;

            foreach (var recipe in recipes)
            {
                recipe.Key = GuidEncoder.Encode(Guid.NewGuid());
            }
            recipeRepo.Upsert(recipes).Wait();

            Console.WriteLine("done");
            Console.ReadLine();
        }
    }

    public static class GuidEncoder
    {
        public static string Encode(string guidText)
        {
            Guid guid = new Guid(guidText);
            return Encode(guid);
        }

        public static string Encode(Guid guid)
        {
            string enc = Convert.ToBase64String(guid.ToByteArray());
            enc = enc.Replace("/", "_");
            enc = enc.Replace("+", "-");
            return enc.Substring(0, 22);
        }

        public static Guid Decode(string encoded)
        {
            encoded = encoded.Replace("_", "/");
            encoded = encoded.Replace("-", "+");
            byte[] buffer = Convert.FromBase64String(encoded + "==");
            return new Guid(buffer);
        }
    }
}
