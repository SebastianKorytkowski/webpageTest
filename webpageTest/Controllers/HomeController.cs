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
            {
                var meals = db.GetUserMeals(User.Identity);
                var exercises = db.GetUserExercises(User.Identity);

                var today = DateTime.Now.Date;

                var todayMeals = meals.Where(m => DbFunctions.TruncateTime(m.Date) == today).ToList();
                var todayExercises = exercises.Where(e => DbFunctions.TruncateTime(e.Date) == today).ToList();

                return View(new IndexViewModel
                {
                    Info = $"",
                    TodayMeals = todayMeals,
                    TodayExercises = todayExercises
                });
            }


            return View(new IndexViewModel());
        }
    }
}