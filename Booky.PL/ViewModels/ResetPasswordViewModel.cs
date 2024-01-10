using System.ComponentModel.DataAnnotations;

namespace Booky.PL.ViewModels
{
    public class ResetPasswordViewModel
    {
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Password is Required !!")]
        [DataType(DataType.Password)]
        public string newPassword { get; set; }

        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Confirm Password is Required !!")]
        [DataType(DataType.Password)]
        [Compare("newPassword", ErrorMessage = "Confirm password match the password")]
        public string ConfirmPassword { get; set; }
    }
}
