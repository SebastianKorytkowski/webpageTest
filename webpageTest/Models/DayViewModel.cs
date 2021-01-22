using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webpageTest.Models
{
    public class DayViewModel
    {
        public DateTime Date { get; set; }
        public IEnumerable<Excercise> Exercises { get; set; }
        public IEnumerable<Meal> Meals { get; set; }
        public string Info { get; set; }
    }
}