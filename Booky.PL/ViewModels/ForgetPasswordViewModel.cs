using System.ComponentModel.DataAnnotations;

namespace Booky.PL.ViewModels
{
    public class ForgetPasswordViewModel
    {
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Email is Required !!")]
        [EmailAddress(ErrorMessage = "invalid Email Address !!")]
        public string Email { get; set; }
    }
}
