using webpageTest.Models;

namespace webpageTest.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<webpageTest.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(webpageTest.Models.ApplicationDbContext context)
        {

            context.Ingredients.AddOrUpdate(
                  p => p.Name,
                  new Ingredient { Name = "Banana", CalPerGram = 95 },
                  new Ingredient { Name = "Chips", CalPerGram = 119 },
                  new Ingredient { Name = "Chocolate", CalPerGram = 549 },
                  new Ingredient { Name = "Scrambled eggs", CalPerGram = 119 },
                  new Ingredient { Name = "Pork chop", CalPerGram = 351 },
                  new Ingredient { Name = "Fried chicken", CalPerGram = 179 },
                  new Ingredient { Name = "Pasta", CalPerGram = 377 },
                  new Ingredient { Name = "Pancakes", CalPerGram = 242 },
                  new Ingredient { Name = "Sausage", CalPerGram = 347 },
                  new Ingredient { Name = "Cola", CalPerGram = 43 },
                  new Ingredient { Name = "Cheese", CalPerGram = 222 },
                  new Ingredient { Name = "Potatoes", CalPerGram = 85 },
                  new Ingredient { Name = "Salmon", CalPerGram = 326 },
                  new Ingredient { Name = "Sandwich", CalPerGram = 38 }
                );

            context.ExcerciseTypes.AddOrUpdate(
                p => p.Name,
                new ExcerciseType { Name = "Aerobik", CalPerSec = 550 },
                new ExcerciseType { Name = "Running", CalPerSec = 1000 },
                new ExcerciseType { Name = "Voleyball", CalPerSec = 400 },
                new ExcerciseType { Name = "Football", CalPerSec = 650 },
                new ExcerciseType { Name = "Skating", CalPerSec = 400 },
                new ExcerciseType { Name = "Gym", CalPerSec = 500 }


            );
        }
    }
}
