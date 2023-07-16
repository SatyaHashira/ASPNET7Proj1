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

        public async Task<BlogPost?> DeleteAsync(Guid Id)
        {
            var existingBlog = await bloggieDbContext.BlogPosts.FindAsync(Id);

            if (existingBlog != null)
            {
                bloggieDbContext.BlogPosts.Remove(existingBlog);
                await bloggieDbContext.SaveChangesAsync();
                return existingBlog;
            }
            return null;
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            return await bloggieDbContext.BlogPosts.Include(s => s.tags).ToListAsync();
        }

        public async Task<BlogPost?> GetAsync(Guid Id)
        {
            return await bloggieDbContext.BlogPosts.Include(s => s.tags).FirstOrDefaultAsync(s => s.Id == Id);
        }

        public async Task<BlogPost> GetByUrlHandleAsync(string urlHandle)
        {
            return await bloggieDbContext.BlogPosts.Include(s=>s.tags).FirstOrDefaultAsync(s => s.UrlHandle == urlHandle);
        }

        public async Task<BlogPost?> UpdateAsync(BlogPost blogPost)
        {
            var ExistingBlogPost = await bloggieDbContext.BlogPosts.Include(s => s.tags).
                FirstOrDefaultAsync(s => s.Id == blogPost.Id);
            if (ExistingBlogPost != null)
            {
                ExistingBlogPost.Id = blogPost.Id;
                ExistingBlogPost.Heading = blogPost.Heading;
                ExistingBlogPost.PageTitle = blogPost.PageTitle;
                ExistingBlogPost.Content = blogPost.Content;
                ExistingBlogPost.ShortDescription = blogPost.ShortDescription;
                ExistingBlogPost.FeaturedImageUrl = blogPost.FeaturedImageUrl;
                ExistingBlogPost.UrlHandle = blogPost.UrlHandle;
                ExistingBlogPost.PublishedDate = blogPost.PublishedDate;
                ExistingBlogPost.Author = blogPost.Author;
                ExistingBlogPost.visible = blogPost.visible;
                ExistingBlogPost.tags=blogPost.tags;

                await bloggieDbContext.SaveChangesAsync();
                return ExistingBlogPost;
            }
            return null;
        }
    }
}
