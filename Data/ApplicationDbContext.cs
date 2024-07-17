using BlogSite.Entity;
using Microsoft.EntityFrameworkCore;

namespace BlogSite.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<BlogModel> Blog { get; set; }
        public DbSet<CategoryModel> Category {get; set;}
        public DbSet<UserModel> User {get; set;}
    }
}