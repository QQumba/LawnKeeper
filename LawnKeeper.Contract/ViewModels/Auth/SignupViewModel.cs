using System.ComponentModel.DataAnnotations;

namespace LawnKeeper.Contract.ViewModels.Auth
{
    public class SignupViewModel
    {
        [Required]
        [EmailAddress(ErrorMessage = "Email is not valid.")]
        public string Email { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Compare(nameof(Password), ErrorMessage = "Password don't match.")]
        public string PasswordConfirmation { get; set; }
    }
}