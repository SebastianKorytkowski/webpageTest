using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webpageTest.Models;

namespace webpageTest.Controllers
{
    [Authorize]
    public class CalendarController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Calendar
        public ActionResult Index(DateTime?start=null, int daysCount=7)
        {
            DateTime realStart = start ?? DateTime.Now;

            List<Day> days = new List<Day>();

            var meals = db.GetUserMeals(User.Identity);
            var exercises = db.GetUserExercises(User.Identity);


            for (DateTime date = realStart; date > realStart - TimeSpan.FromDays(daysCount); date-=TimeSpan.FromDays(1))
            {
                days.Add(new Day
                {
                    Date = date, 
                    Exercises = exercises.Where(d=> DbFunctions.TruncateTime(d.Date)==date).ToList(),
                    Meals = meals.Where(d => DbFunctions.TruncateTime(d.Date) == date).ToList()
                });
            }


            return View(new CalendarViewModel{Days = days, Start = realStart});
        }
    }
}