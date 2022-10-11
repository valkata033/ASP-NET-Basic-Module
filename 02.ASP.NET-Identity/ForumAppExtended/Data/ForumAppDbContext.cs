using ForumAppExtended.Data.Configure;
using ForumAppExtended.Data.Entities;
using ForumAppExtended.Data.Entities.Account;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ForumAppExtended.Data
{
    public class ForumAppDbContext : IdentityDbContext<ApplicationUser>
    {
        public ForumAppDbContext(DbContextOptions<ForumAppDbContext> options)
                   : base(options)
        {
        }

        public DbSet<Post> Posts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new PostsConfiguration());

            builder.Entity<Post>()
                .Property(p => p.IsDeleted)
                .HasDefaultValue(false);

            base.OnModelCreating(builder);
        }
    }
}