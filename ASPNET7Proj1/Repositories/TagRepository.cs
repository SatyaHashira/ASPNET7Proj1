using ASPNET7Proj1.Data;
using ASPNET7Proj1.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace ASPNET7Proj1.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly BloggieDbContext bloggieDbContext;

        public TagRepository(BloggieDbContext bloggieDbContext)
        {
            this.bloggieDbContext = bloggieDbContext;
        }

        public async Task<Tags> AddAsync(Tags tag)
        {
            await bloggieDbContext.Tags.AddAsync(tag);
            await bloggieDbContext.SaveChangesAsync();
            return tag;
        }

        public async Task<Tags?> DeleteAsync(Guid Id)
        {
            var ExistingTag = await bloggieDbContext.Tags.FindAsync(Id);
            if (ExistingTag != null)
            {
                bloggieDbContext.Tags.Remove(ExistingTag);
                await bloggieDbContext.SaveChangesAsync();
                return ExistingTag;
            }
            return null;
        }

        public async Task<IEnumerable<Tags>> GetAllAsync()
        {
            return await bloggieDbContext.Tags.ToListAsync();
        }

        public async Task<Tags?> GetAsync(Guid Id)
        {
            return await bloggieDbContext.Tags.FirstOrDefaultAsync(s => s.Id == Id);
        }

        public async Task<Tags?> UpdateAsync(Tags tag)
        {
            var ExistingTag = await bloggieDbContext.Tags.FindAsync(tag.Id);

            if (ExistingTag != null)
            {
                ExistingTag.Name = tag.Name;
                ExistingTag.DisplayName = tag.DisplayName;

                await bloggieDbContext.SaveChangesAsync();

                return ExistingTag;
            }
            return null;
        }
    }
}
