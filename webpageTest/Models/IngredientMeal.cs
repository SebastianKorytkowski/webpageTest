namespace webpageTest.Models
{
    public class IngredientMeal
    {
        public int Id { get; set; }

        public Meal Meal { get; set; }

        public virtual Ingredient Ingredient { get; set; }

        public float Quantity { get; set; }

        public float Calories => Ingredient.CalPerGram * Quantity / 100.0f;
    }
}