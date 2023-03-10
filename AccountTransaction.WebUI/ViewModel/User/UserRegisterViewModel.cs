using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AccountTransaction.WebUI.ViewModel.User
{
    public class UserRegisterViewModel
    {
        [Required(ErrorMessage = "The field {0} is required")]
        [DisplayName("Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [DisplayName("Social Number")]
        public string SocialNumber { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [EmailAddress(ErrorMessage = "Invalid format of field {0}")]
        [DisplayName("E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [StringLength(100, ErrorMessage = "The field {0} should be between {2} and {1} chars", MinimumLength = 6)]
        public string Password { get; set; }

        [DisplayName("Confirm your password")]
        [Compare("Password", ErrorMessage = "The password must match.")]
        public string ConfirmPassword { get; set; }
    }
}