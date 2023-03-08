using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AccountTransaction.WebUI.ViewModel.User
{
    public class UserLoginViewModel
    {
        [Required(ErrorMessage = "The field {0} is required")]
        [EmailAddress(ErrorMessage = "Invalid format of field {0}")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [StringLength(100, ErrorMessage = "The field {0} should be between {2} and {1} chars", MinimumLength = 6)]
        public string Password { get; set; }
        public string? ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }
}
