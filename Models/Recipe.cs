using System.ComponentModel.DataAnnotations;

namespace NutriVision.Models
{
    public class Recipe
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string CuisineType { get; set; }

        [Range(1, 20)]
        public int Servings { get; set; }

        [Range(1, 300)]
        public int PreparationTime { get; set; }

        public string Difficulty { get; set; }

        public string ImageUrl { get; set; }

        public ICollection<RecipeIngredient> RecipeIngredients { get; set; }
            = new List<RecipeIngredient>();
    }
}