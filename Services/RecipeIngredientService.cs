using Microsoft.EntityFrameworkCore;
using NutriVision.Data;
using NutriVision.Models;

namespace NutriVision.Services
{
    public class RecipeIngredientService
    {
        private readonly AppDbContext _context;

        public RecipeIngredientService(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddIngredientToRecipeAsync(RecipeIngredient recipeIngredient)
        {
            _context.RecipeIngredients.Add(recipeIngredient);

            await _context.SaveChangesAsync();
        }

        public async Task<List<RecipeIngredient>> GetRecipeIngredientsAsync(int recipeId)
        {
            return await _context.RecipeIngredients
                .Include(ri => ri.Ingredient)
                .Where(ri => ri.RecipeId == recipeId)
                .ToListAsync();
        }

        public async Task<double> CalculateRecipeCaloriesAsync(int recipeId)
        {
            var items = await _context.RecipeIngredients
                .Include(ri => ri.Ingredient)
                .Where(ri => ri.RecipeId == recipeId)
                .ToListAsync();

            double totalCalories = 0;

            foreach (var item in items)
            {
                totalCalories += item.Quantity
                    * item.Ingredient.CaloriesPerUnit;
            }

            return totalCalories;
        }

        public async Task<string> GetRecipeInsightAsync(int recipeId)
        {
            var items = await _context.RecipeIngredients
                .Include(ri => ri.Ingredient)
                .Where(ri => ri.RecipeId == recipeId)
                .ToListAsync();

            double totalProtein = 0;
            double totalCarbs = 0;
            double totalFat = 0;
            double totalCalories = 0;

            foreach (var item in items)
            {
                totalProtein += item.Quantity * item.Ingredient.Protein;

                totalCarbs += item.Quantity * item.Ingredient.Carbs;

                totalFat += item.Quantity * item.Ingredient.Fat;

                totalCalories += item.Quantity
                    * item.Ingredient.CaloriesPerUnit;
            }

            if (totalProtein > 50)
            {
                return "🔥 High Protein Meal";
            }

            if (totalCalories < 400)
            {
                return "🥗 Light Meal";
            }

            if (totalCarbs < 20)
            {
                return "🥩 Low Carb Meal";
            }

            return "⚖️ Balanced Meal";
        }
    }
}