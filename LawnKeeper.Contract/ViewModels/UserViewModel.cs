using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LawnKeeper.Contract.ViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }

        public List<LawnViewModel> Lawns { get; set; }
    }
}