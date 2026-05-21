using Microsoft.EntityFrameworkCore;
using NutriVision.Data;
using NutriVision.Models;

namespace NutriVision.Services
{
    public class RecipeService
    {
        private readonly AppDbContext _context;

        public RecipeService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Recipe>> GetRecipesAsync()
        {
            return await _context.Recipes.ToListAsync();
        }

        public async Task AddRecipeAsync(Recipe recipe)
        {
            _context.Recipes.Add(recipe);

            await _context.SaveChangesAsync();
        }

        public async Task<List<Recipe>> SearchRecipesAsync(
        string searchTerm,
        string category,
        string cuisine)
        {
            var query = _context.Recipes.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(r =>
                    r.Name.Contains(searchTerm));
            }

            if (!string.IsNullOrWhiteSpace(category))
            {
                query = query.Where(r =>
                    r.Category == category);
            }

            if (!string.IsNullOrWhiteSpace(cuisine))
            {
                query = query.Where(r =>
                    r.CuisineType == cuisine);
            }

            return await query.ToListAsync();
        }
        public async Task DeleteRecipeAsync(int id)
        {
            var recipe = await _context.Recipes.FindAsync(id);

            if (recipe != null)
            {
                _context.Recipes.Remove(recipe);

                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateRecipeAsync(Recipe recipe)
        {
            _context.Recipes.Update(recipe);

            await _context.SaveChangesAsync();
        }
    }
}