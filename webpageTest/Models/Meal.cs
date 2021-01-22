using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace webpageTest.Models
{
    public class Meal
    {
        public int Id { get; set; }


        [Required, DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public virtual ICollection<IngredientMeal> IngredientMeals { get; set; }

        public float Calories => IngredientMeals==null?0:IngredientMeals.Aggregate(0.0f, (a, b) => a + b.Calories);
        
        public string Summary => (IngredientMeals==null|| IngredientMeals.Count==0) ? "Empty meal" :$"{string.Join(", ", IngredientMeals.Select(t=>t.Ingredient.Name + $"({t.Quantity}g)"))}; eaten  {Calories} KCal";
    }
}