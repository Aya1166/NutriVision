using System.ComponentModel.DataAnnotations;

namespace NutriVision.Models
{
    public class Ingredient
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Range(0, 2000)]
        public int CaloriesPerUnit { get; set; }

        [Range(0, 100)]
        public double Protein { get; set; }

        [Range(0, 100)]
        public double Carbs { get; set; }

        [Range(0, 100)]
        public double Fat { get; set; }

        [Required]
        public string Unit { get; set; }

        public ICollection<RecipeIngredient> RecipeIngredients { get; set; }
            = new List<RecipeIngredient>();
    }
}