namespace webpageTest.Models
{
    public class IngredientMeal
    {
        public int Id { get; set; }

        public Meal Meal { get; set; }

        public Ingredient Ingredient { get; set; }

        public float Quantity { get; set; }
    }
}