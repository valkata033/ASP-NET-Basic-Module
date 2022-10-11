using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using ForumAppExtended.Data.Entities;

namespace ForumAppExtended.Data.Configure
{
    public class PostsConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            List<Post> posts = GetPosts();

            builder.HasData(posts);
        }

        private List<Post> GetPosts()
        {
            return new List<Post>()
            {
                new Post()
                {
                    Id = 1,
                    Title = "My first post",
                    Content = "My first post will be about CRUD operations yayyyyyy!"
                },
                new Post()
                {
                    Id = 2,
                    Title = "My second post",
                    Content = "This is my second post. A am getting better!!"
                },
                new Post()
                {
                    Id = 3,
                    Title = "My third post",
                    Content = "Hello from my third post. CRUD operations are very interesting!!"
                }
            };
        }
    }
}
