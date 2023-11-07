using BuzzBlog.Data;
using BuzzBlog.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BuzzBlog.Repositories
{
    public class BlogPostRepository : IBlogPostrepository
    {
        private readonly BuzzBlogDbContext _buzzBlogDbContext;

        public BlogPostRepository(BuzzBlogDbContext buzzBlogDbContext)
        {
            _buzzBlogDbContext = buzzBlogDbContext;
        }
        public async Task<BlogPost> AddAsync(BlogPost blogpost)
        {
           await _buzzBlogDbContext.BlogPosts.AddAsync(blogpost);
           await _buzzBlogDbContext.SaveChangesAsync();
            return blogpost;
        }

        public async Task<BlogPost> DeleteAsync(Guid id)
        {
            var existingBlogPost = await _buzzBlogDbContext.BlogPosts.FindAsync(id);

            if(existingBlogPost != null)
            {
                _buzzBlogDbContext.BlogPosts.Remove(existingBlogPost);
                await _buzzBlogDbContext.SaveChangesAsync();  
                return existingBlogPost;
            }

            return null;

        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
           return await _buzzBlogDbContext.BlogPosts.ToListAsync();
            
        }

        public async Task<BlogPost> GetAsync(Guid id)
        {
            return await _buzzBlogDbContext.BlogPosts.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<BlogPost> UpdateAsync(BlogPost blogpost)
        {
            var existingTag = await _buzzBlogDbContext.BlogPosts.FindAsync();
            
            if (existingTag != null)
            {
                existingTag.Heading = blogpost.Heading;
                existingTag.PageTitle = blogpost.PageTitle;
                existingTag.Content = blogpost.Content;
                existingTag.ShortDescription = blogpost.ShortDescription;
                existingTag.FeaturedImageUrl = blogpost.FeaturedImageUrl;
                existingTag.UrlHandle = blogpost.UrlHandle;
                existingTag.Author = blogpost.Author;
                existingTag.Visible = blogpost.Visible;

                await _buzzBlogDbContext.SaveChangesAsync();
                return existingTag;
                
            }
            return null;
        }
    }
}
