using System.ComponentModel.DataAnnotations;

namespace ShortUrl.Web.ViewModels
{
    public class SignInViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remeber me?")]
        public bool RememberMe { get; set; }
    }
}
