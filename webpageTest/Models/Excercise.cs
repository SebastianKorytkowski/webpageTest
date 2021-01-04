using System;
using System.ComponentModel.DataAnnotations;

namespace webpageTest.Models
{
    public class Excercise
    {
        public int Id { get; set; }

        public DateTime Time { get; set; }

        public ExcerciseType ExType { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

    }
}