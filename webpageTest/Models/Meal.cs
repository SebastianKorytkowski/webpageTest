using System;

namespace webpageTest.Models
{
    public class Meal
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
    }
}