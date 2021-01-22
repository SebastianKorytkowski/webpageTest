using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webpageTest.Models;

namespace webpageTest.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        public ActionResult Index()
        {
            ApplicationUser currentApplicationUser = db.GetCurrentApplicationUser(User.Identity);
            if (currentApplicationUser != null)
                return RedirectToAction("Day");

            return View();
        }

        [Authorize]
        public ActionResult Day(DateTime?date=null)
        {
            var meals = db.GetUserMeals(User.Identity);
            var exercises = db.GetUserExercises(User.Identity);

            DateTime realDate = (date ?? DateTime.Now).Date;

            var todayMeals = meals.Where(m => DbFunctions.TruncateTime(m.Date) == realDate).OrderBy(m => m.Date).ToList();
            var todayExercises = exercises.Where(e => DbFunctions.TruncateTime(e.Date) == realDate).OrderBy(m => m.Date).ToList();

            float mealsCalories = todayMeals.Aggregate(0.0f, (f, meal) => f + meal.Calories);
            float exercisesCalories = todayExercises.Aggregate(0.0f, (f, exercise) => f + exercise.Calories);

            return View(new DayViewModel
            {
                Date = realDate,
                Info = $"Eaten: {mealsCalories} Kcal\n" +
                       $"Burned: -{exercisesCalories} Kcal\n" +
                       $"Balance: {mealsCalories - exercisesCalories} Kcal",
                Meals = todayMeals,
                Exercises = todayExercises
            });
        }
    }
}