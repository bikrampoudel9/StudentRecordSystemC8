using System.ComponentModel.DataAnnotations;

namespace StudentMangementSystemC8.Models
{
    public class UserRegisterDto
    {
        [Required(ErrorMessage = "First name is required")]
        public string? FirstName { get ; set; }

        [Required]
        public string? LastName { get; set; }

        public string? Address { get; set; }    

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }

        [Compare(nameof(Password),ErrorMessage = "Password and Confirm Password doesnt match")]
        public string? ConfirmPassword { get; set; }
    }
}
