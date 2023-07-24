using ASPNET7Proj1.Data;
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
        public async Task<int> GetTotalLikes(Guid blogPostId)
        {
             return await bloggieDbContext.BlogPostLike.
                CountAsync(s => s.BlogPostId == blogPostId);
        }
    }
}
