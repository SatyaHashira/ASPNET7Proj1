using ASPNET7Proj1.Data;
using ASPNET7Proj1.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace ASPNET7Proj1.Repositories
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly BloggieDbContext bloggieDbContext;
        public BlogPostRepository(BloggieDbContext bloggieDbContext)
        {
            this.bloggieDbContext = bloggieDbContext;
        }
        public async Task<BlogPost> AddAsync(BlogPost blogPost)
        {
            await bloggieDbContext.BlogPosts.AddAsync(blogPost);
            await bloggieDbContext.SaveChangesAsync();
            return blogPost;
        }

        public Task<BlogPost?> DeleteAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            return await bloggieDbContext.BlogPosts.Include(s=>s.tags).ToListAsync();
        }

        public async Task<BlogPost?> GetAsync(Guid Id)
        {
            return await bloggieDbContext.BlogPosts.Include(s=>s.tags).FirstOrDefaultAsync(s => s.Id == Id);
        }

        public Task<BlogPost?> UpdateAsync(BlogPost blogPost)
        {
            throw new NotImplementedException();
        }
    }
}
