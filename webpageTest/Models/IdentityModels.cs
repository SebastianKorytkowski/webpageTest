using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using WebGrease.Css.Extensions;
using webpageTest.Models;

namespace webpageTest.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            return userIdentity;
        }
    }

    namespace IdentityExtension
    {
        public static class IdentityExtensions
        {
            public static void AddUpdateClaim(this IPrincipal currentPrincipal, string key, string value)
            {
                var identity = currentPrincipal.Identity as ClaimsIdentity;
                if (identity == null)
                    return;

                // check for existing claim and remove it
                var existingClaim = identity.FindFirst(key);
                if (existingClaim != null)
                    identity.RemoveClaim(existingClaim);

                // add new claim
                identity.AddClaim(new Claim(key, value));
                var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
                authenticationManager.AuthenticationResponseGrant =
                    new AuthenticationResponseGrant(new ClaimsPrincipal(identity),
                        new AuthenticationProperties() {IsPersistent = true});
            }

            public static string GetClaimValue(this IPrincipal currentPrincipal, string key)
            {
                var identity = currentPrincipal.Identity as ClaimsIdentity;
                if (identity == null)
                    return null;

                var claim = identity.Claims.FirstOrDefault(c => c.Type == key);
                return claim.Value;
            }

            public static string GetFontColor(this IIdentity identity)
            {
                var claim = ((ClaimsIdentity) identity).FindFirst("FontColor");
                // Test for null to avoid issues during local testing
                return (claim != null) ? claim.Value : ColorTranslator.ToHtml(Color.Black);
            }
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

        public IQueryable<Meal> GetUserMeals(IIdentity user)
        {
            string userId = user.GetUserId();

            var meals = from m in Meals
                join u in Users
                    on m.ApplicationUser equals u
                where u.Id == userId
                        select m;

            return meals;
        }

        public IQueryable<Excercise> GetUserExercises(IIdentity user)
        {
            string userId = user.GetUserId();

            var excercises = from e in Excercises
                join u in Users
                    on e.ApplicationUser equals u
                where u.Id == userId
                select e;

            return excercises;
        }

        public IQueryable<IngredientMeal> GetMealIngridients(Meal meal)
        {
            if (meal == null) return null;

            var mealIngredients = from m in Meals
                join im in IngredientsMeals
                    on m.Id equals im.Meal.Id
                where m.Id == meal.Id
                select im;
            return mealIngredients;
        }

        public DbSet<Excercise> Excercises { get; set; }
        public DbSet<ExcerciseType> ExcerciseTypes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<IngredientMeal> IngredientsMeals { get; set; }
        public DbSet<Meal> Meals { get; set; }
    }
}