using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webpageTest.Models
{
    public class IndexViewModel
    {
        public IEnumerable<Excercise> TodayExercises { get; set; }
        public IEnumerable<Meal> TodayMeals { get; set; }

        public string Info { get; set; }
    }
}