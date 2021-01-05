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
                  new Ingredient { Name = "Banan", CalPerGram = 95 },
                  new Ingredient { Name = "Bigos", CalPerGram = 119 },
                  new Ingredient { Name = "Czekolada", CalPerGram = 549 },
                  new Ingredient { Name = "Jajecznica", CalPerGram = 119 },
                  new Ingredient { Name = "Kotlet schabowy", CalPerGram = 351 },
                  new Ingredient { Name = "Kurczak pieczony", CalPerGram = 179 },
                  new Ingredient { Name = "Makaron", CalPerGram = 377 },
                  new Ingredient { Name = "Nale�niki z serem", CalPerGram = 242 },
                  new Ingredient { Name = "Par�wki", CalPerGram = 347 },
                  new Ingredient { Name = "Cola", CalPerGram = 43 },
                  new Ingredient { Name = "Ser topiony", CalPerGram = 222 },
                  new Ingredient { Name = "Ziemniaki", CalPerGram = 85 },
                  new Ingredient { Name = "W�gorz w�dzony", CalPerGram = 326 },
                  new Ingredient { Name = "Burak", CalPerGram = 38 }
                );

            context.ExcerciseTypes.AddOrUpdate(
                p => p.Name,
                new ExcerciseType { Name = "Aerobik", CalPerSec = 550 },
                new ExcerciseType { Name = "Bieg", CalPerSec = 1000 },
                new ExcerciseType { Name = "Si�ownia", CalPerSec = 400 },
                new ExcerciseType { Name = "Pi�ka no�na", CalPerSec = 650 },
                new ExcerciseType { Name = "Rolki", CalPerSec = 400 },
                new ExcerciseType { Name = "Wios�owanie", CalPerSec = 500 }


            );
        }
    }
}
