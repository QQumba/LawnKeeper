using System.ComponentModel.DataAnnotations;

namespace LawnKeeper.Contract.ViewModels.Auth
{
    public class LoginViewModel
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}