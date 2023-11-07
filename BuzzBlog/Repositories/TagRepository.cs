using BuzzBlog.Data;
using BuzzBlog.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BuzzBlog.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly BuzzBlogDbContext buzzBlogDbContext;

        public TagRepository(BuzzBlogDbContext buzzBlogDbContext)
        {
            this.buzzBlogDbContext = buzzBlogDbContext;
        }

        public async Task<Tag> AddAsync(Tag tag)
        {
            await buzzBlogDbContext.Tags.AddAsync(tag);
            await buzzBlogDbContext.SaveChangesAsync();
            return tag;
        }

        public async Task<Tag?> DeleteAsync(Guid id)
        {
            var existingTag = await buzzBlogDbContext.Tags.FindAsync(id);

            if (existingTag != null)
            {
                buzzBlogDbContext.Tags.Remove(existingTag);
                await buzzBlogDbContext.SaveChangesAsync();
                return existingTag;
            }

            return null;
        }   

        public async Task<IEnumerable<Tag>> GetAllAsync()
        {
           return await buzzBlogDbContext.Tags.ToListAsync();
        }

        public async Task<Tag?> GetAsync(Guid id)
        {
            return await buzzBlogDbContext.Tags.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Tag?> UpdateAsync(Tag tag)
        {
            var existingTag = await buzzBlogDbContext.Tags.FindAsync(tag.Id);

            if (existingTag != null)
            {
                existingTag.Name = tag.Name;
                existingTag.DisplayName = tag.DisplayName;

                await buzzBlogDbContext.SaveChangesAsync();

                return existingTag;
            }

            return null;
        }
    }
}
