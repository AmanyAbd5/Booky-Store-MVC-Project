using System.ComponentModel.DataAnnotations;

namespace Booky.PL.ViewModels
{
    public class LoginViewModel
    {
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Email is Required !!")]
        [EmailAddress(ErrorMessage = "invalid Email Address !!")]
        public string Email { get; set; }

        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Password is Required !!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
