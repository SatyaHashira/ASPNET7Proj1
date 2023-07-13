using ASPNET7Proj1.Models.Domain;

namespace ASPNET7Proj1.Repositories
{
    public interface ITagRepository
    {
        Task<IEnumerable<Tags>> GetAllAsync();

        Task<Tags?> GetAsync(Guid Id);

        Task<Tags> AddAsync(Tags tag);

        Task<Tags?> UpdateAsync(Tags tag);

        Task<Tags?> DeleteAsync(Guid Id);


    }
}
