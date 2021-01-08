using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webpageTest.Models
{
    public class Meal
    {
        public int Id { get; set; }


        [Required, DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
    }
}