using System.ComponentModel.DataAnnotations;

namespace NutriVision.Models
{
    public class RecipeIngredient
    {
        public int RecipeId { get; set; }

        public Recipe Recipe { get; set; }

        public int IngredientId { get; set; }

        public Ingredient Ingredient { get; set; }

        [Range(1, 10000)]
        public double Quantity { get; set; }
    }
}