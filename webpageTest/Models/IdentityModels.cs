using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace webpageTest.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public ApplicationUser GetCurrentApplicationUser(IIdentity user)
        {
            if (!user.IsAuthenticated) return null;

            string currentUserId = user.GetUserId();
            return Users.FirstOrDefault(x => x.Id == currentUserId);
        }

        public DbSet<Excercise> Excercises { get; set; }
        public DbSet<ExcerciseType> ExcerciseTypes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<IngredientMeal> IngredientsMeals { get; set; }
        public DbSet<Meal> Meals { get; set; }
    }
}