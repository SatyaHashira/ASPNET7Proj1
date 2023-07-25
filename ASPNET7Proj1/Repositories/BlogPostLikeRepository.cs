using ASPNET7Proj1.Data;
using ASPNET7Proj1.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace ASPNET7Proj1.Repositories
{
    public class BlogPostLikeRepository : IBlogPostLikeRepository
    {
        private readonly BloggieDbContext bloggieDbContext;
        public BlogPostLikeRepository(BloggieDbContext bloggieDbContext)
        {
            this.bloggieDbContext = bloggieDbContext;
        }

        public async Task<BlogPostLike> AddLikeForBlog(BlogPostLike blogPostLike)
        {
            await bloggieDbContext.BlogPostLike.AddAsync(blogPostLike);
            await bloggieDbContext.SaveChangesAsync();
            return blogPostLike;
        }

        public Task<IEnumerable<BlogPostLike>> GetLikesForBlog(Guid blogPostId)
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetTotalLikes(Guid blogPostId)
        {
             return await bloggieDbContext.BlogPostLike.
                CountAsync(s => s.BlogPostId == blogPostId);
        }
    }
}
