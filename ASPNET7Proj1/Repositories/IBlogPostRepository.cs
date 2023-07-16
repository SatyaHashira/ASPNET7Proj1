using ASPNET7Proj1.Models.Domain;

namespace ASPNET7Proj1.Repositories
{
    public interface IBlogPostRepository
    {
        Task<IEnumerable<BlogPost>> GetAllAsync();
        Task<BlogPost?> GetAsync(Guid Id);
        Task<BlogPost> AddAsync(BlogPost blogPost);
        Task<BlogPost?> UpdateAsync(BlogPost blogPost);
        Task<BlogPost?> DeleteAsync(Guid Id);
        Task<BlogPost> GetByUrlHandleAsync(string urlHandle);
    }
}
