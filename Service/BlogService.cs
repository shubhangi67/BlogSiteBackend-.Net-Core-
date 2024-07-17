using BlogSite.Data;
using BlogSite.Entity;
using BlogSite.ViewModel.Blog;
using Microsoft.EntityFrameworkCore;

namespace BlogSite.Service
{
    public class BlogService
    {
        private readonly ApplicationDbContext _context;

        public BlogService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> CreateBlogAsync(BlogCreateViewModel input)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }
            var categoryId = new Guid(input.CategoryId.ToString());
            var category = await _context.Category.SingleOrDefaultAsync(x => x.Id == categoryId);

            if (category == null)
            {
                // Handle the case where the category was not found
                return "Category not found.";
            }
            var blog = new BlogModel
            {
                Description = input.Description,
                Title = input.Title,
                Content = input.Content,
                Image = input.Image,
                IsFeatured = input.IsFeatured,
                CategoryId = input.CategoryId,
                CreatedOn = DateTime.Now
            };

            _context.Blog.Add(blog);
            await _context.SaveChangesAsync();
            return "Blog created successfully.";
        }
        public async Task<BlogModel> GetBlogAsync(int id)
        {
            return await _context.Blog.FindAsync(id);
        }

        public async Task<IEnumerable<BlogModel>> GetAllBlogsAsync()
        {
            return await _context.Blog.ToListAsync();
        }

        public async Task<string> UpdateBlogAsync(int id, BlogUpdateViewModel input)
        {
            var blog = await _context.Blog.FindAsync(id);
            if (blog == null)
            {
                throw new KeyNotFoundException("Blog not found");
            }
            var categoryId = new Guid(input.CategoryId.ToString());
            var category = await _context.Category.SingleOrDefaultAsync(x => x.Id == categoryId);

            if (category == null)
            {
                // Handle the case where the category was not found
                return "Category not found.";
            }
            blog.Title = input.Title;
            blog.Description = input.Description;
            blog.Content = input.Content;
            blog.Image = input.Image;
            blog.IsFeatured = input.IsFeatured;
            blog.CategoryId = input.CategoryId;
            blog.ModifiedOn = DateTime.Now;
            await _context.SaveChangesAsync();
            return "Update Blog Successfully";
        }

        public async Task DeleteBlogAsync(int id)
        {
            var blog = await _context.Blog.FindAsync(id);
            if (blog == null)
            {
                throw new KeyNotFoundException("Blog not found");
            }
            _context.Blog.Remove(blog);
            await _context.SaveChangesAsync();
        }
    }

}