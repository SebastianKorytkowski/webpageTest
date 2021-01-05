using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace webpageTest.Models
{
    public class MealViewModel
    {
        public Meal Meal { get; set; }

        public IEnumerable<webpageTest.Models.IngredientMeal> MealIngredients { get; set; }

        public IEnumerable<SelectListItem> AllIngridientsSelectList { get; set; }
    }
}