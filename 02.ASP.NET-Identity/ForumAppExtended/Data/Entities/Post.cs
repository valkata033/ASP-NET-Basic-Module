using System.ComponentModel.DataAnnotations;
using static ForumAppExtended.Data.DataConstants.Post;

namespace ForumAppExtended.Data.Entities
{
    public class Post
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(TitleMaxLength)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(ContentMaxLength)]
        public string Content { get; set; } = null!;

        [Required]
        public bool IsDeleted { get; set; } = false;
    }
}
