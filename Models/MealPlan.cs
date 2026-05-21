using System.ComponentModel.DataAnnotations;

namespace NutriVision.Models
{
    public class MealPlan
    {
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public int RecipeId { get; set; }

        public Recipe Recipe { get; set; }
    }
}