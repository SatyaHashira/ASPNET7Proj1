using ASPNET7Proj1.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace ASPNET7Proj1.Data
{
    public class BloggieDbContext : DbContext
    {
        public BloggieDbContext(DbContextOptions<BloggieDbContext> options) : base(options)
        {
        }
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Models.Domain.Tags> Tags { get; set; }

        
    }
}
