using BuzzBlog.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BuzzBlog.Data
{
    public class BuzzBlogDbContext : DbContext
    {
        public BuzzBlogDbContext(DbContextOptions<BuzzBlogDbContext> options) : base(options)
        {

        }

        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Tag> Tags { get; set; }


    }
}
