using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace Booky.PL.ViewModels
{
    public class RegisterViewModel
    {
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "First Nmae is Required !!")]
        public string Fname { get; set; }

        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Last Nmae is Required !!")]
        public string Lname { get; set; }

        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Email is Required !!")]
        [EmailAddress(ErrorMessage ="invalid Email Address !!")]
        public string Email { get; set; }

        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Password is Required !!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Confirm Password is Required !!")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="Confirm password match the password")]
        public string ConfirmPassword { get; set; }

        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "You have to Agree !!")]
        public bool isAgree { get; set; }
    }
}
