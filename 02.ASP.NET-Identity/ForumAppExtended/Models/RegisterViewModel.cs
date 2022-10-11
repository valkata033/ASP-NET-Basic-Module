using System.ComponentModel.DataAnnotations;

namespace ForumAppExtended.Models
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        [Compare(nameof(PasswordConfirmed))]
        public string Password { get; set; } = null!;

        [Required]
        public string PasswordConfirmed { get; set; } = null!;

        [Required]
        [StringLength(20, MinimumLength = 2)]
        public string FirstName { get; set; } = null!;

        [Required]
        [StringLength(20, MinimumLength = 2)]
        public string LastName { get; set; } = null!;

    }
}
