using CodeBlog.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace CodeBlog.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        //program.cs ile köprü
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
