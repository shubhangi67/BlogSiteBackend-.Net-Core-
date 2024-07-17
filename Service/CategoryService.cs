using BlogSite.Data;
using BlogSite.Entity;
using BlogSite.ViewModel.Category;
using Microsoft.EntityFrameworkCore;

namespace BlogSite.Service
{
    public class CategoryService
    {
        private readonly ApplicationDbContext _context;

        public CategoryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateCategoryAsync(CategoryCreateViewModel input)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            var category = new CategoryModel
            {
                CategoryName = input.CategoryName,
                CreatedOn = DateTime.Now
            };

            _context.Category.Add(category);
            await _context.SaveChangesAsync();
        }
        public async Task<CategoryModel> GetCategoryAsync(Guid id)
        {
            return await _context.Category.FindAsync(id);
        }

        public async Task<List<CategoryModel>> GetAllCategoryAsync()
        {
            return await _context.Category.ToListAsync();
        }

        public async Task UpdateCategoryAsync(Guid id, CategoryUpdateViewModel input)
        {
            var category = await _context.Category.FindAsync(id);
            if (category == null)
            {
                throw new KeyNotFoundException("Blog not found");
            }

            category.CategoryName = input.CategoryName;
            category.ModifiedOn = DateTime.Now;  
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(Guid id)
        {
            var category = await _context.Category.FindAsync(id);
            if (category == null)
            {
                throw new KeyNotFoundException("Category not found");
            }

            _context.Category.Remove(category);
            await _context.SaveChangesAsync();
        }
    }
}