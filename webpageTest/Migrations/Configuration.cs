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
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            context.Ingredients.AddOrUpdate(
                  p => p.Name,
                  new Ingredient { Name = "Banan", CalPerGram = 95 },
                  new Ingredient { Name = "Bigos", CalPerGram = 119 },
                  new Ingredient { Name = "Czekolada", CalPerGram = 549 },
                  new Ingredient { Name = "Jajecznica", CalPerGram = 119 },
                  new Ingredient { Name = "Kotlet schabowy", CalPerGram = 351 },
                  new Ingredient { Name = "Kurczak pieczony", CalPerGram = 179 },
                  new Ingredient { Name = "Makaron", CalPerGram = 377 },
                  new Ingredient { Name = "Naleœniki z serem", CalPerGram = 242 },
                  new Ingredient { Name = "Parówki", CalPerGram = 347 },
                  new Ingredient { Name = "Cola", CalPerGram = 43 },
                  new Ingredient { Name = "Ser topiony", CalPerGram = 222 },
                  new Ingredient { Name = "Ziemniaki", CalPerGram = 85 },
                  new Ingredient { Name = "Wêgorz wêdzony", CalPerGram = 326 },
                  new Ingredient { Name = "Burak", CalPerGram = 38 }
                );

            context.ExcerciseTypes.AddOrUpdate(
                p => p.Name,
                new ExcerciseType { Name = "Aerobik", CalPerSec = 550 },
                new ExcerciseType { Name = "Bieg", CalPerSec = 1000 },
                new ExcerciseType { Name = "Si³ownia", CalPerSec = 400 },
                new ExcerciseType { Name = "Pi³ka no¿na", CalPerSec = 650 },
                new ExcerciseType { Name = "Rolki", CalPerSec = 400 },
                new ExcerciseType { Name = "Wios³owanie", CalPerSec = 500 }


            );
        }
    }
}
