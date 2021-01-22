using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webpageTest.Models
{
    public class Day
    {
        public DateTime Date { get; set; }
        public List<Meal> Meals { get; set; }
        public List<Excercise> Exercises { get; set; }
    }

    public class CalendarViewModel
    {
        public DateTime Start { get; set; }
        public List<Day> Days { get; set; }
    }
}