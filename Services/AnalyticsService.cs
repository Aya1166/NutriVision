using Microsoft.EntityFrameworkCore;
using NutriVision.Data;

namespace NutriVision.Services
{
    public class AnalyticsService
    {
        private readonly AppDbContext _context;

        public AnalyticsService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> GetTotalRecipesAsync()
        {
            return await _context.Recipes.CountAsync();
        }

        public async Task<int> GetTotalIngredientsAsync()
        {
            return await _context.Ingredients.CountAsync();
        }

        public async Task<double> GetAveragePreparationTimeAsync()
        {
            return await _context.Recipes
                .AverageAsync(r => (double?)r.PreparationTime) ?? 0;
        }

        public async Task<List<CategoryStat>> GetRecipesByCategoryAsync()
        {
            return await _context.Recipes
                .GroupBy(r => r.Category)
                .Select(g => new CategoryStat
                {
                    Category = g.Key,
                    Count = g.Count()
                })
                .ToListAsync();
        }

        public async Task<List<CuisineStat>> GetCuisineDistributionAsync()
        {
            return await _context.Recipes
                .GroupBy(r => r.CuisineType)
                .Select(g => new CuisineStat
                {
                    Cuisine = g.Key,
                    Count = g.Count()
                })
                .ToListAsync();
        }
    }

    public class CategoryStat
    {
        public string Category { get; set; }

        public int Count { get; set; }
    }

    public class CuisineStat
    {
        public string Cuisine { get; set; }

        public int Count { get; set; }
    }
}