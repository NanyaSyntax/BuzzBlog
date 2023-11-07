using BuzzBlog.Models.Domain;
using System.Collections;

namespace BuzzBlog.Repositories
{
    public interface IBlogPostrepository
    {
        Task<IEnumerable<BlogPost>> GetAllAsync();

        Task<BlogPost> GetAsync(Guid id);

        Task<BlogPost> AddAsync(BlogPost blogpost);

        Task<BlogPost> UpdateAsync(BlogPost blogpost);

        Task<BlogPost> DeleteAsync(Guid id);
    }
}
