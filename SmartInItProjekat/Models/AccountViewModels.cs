using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SmartInItProjekat.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [EmailAddress(ErrorMessage = "Invalid e-mail address.")]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [StringLength(maximumLength: 50, MinimumLength = 4, ErrorMessage = "User name must be between 4 and 50 characters!")]
        [RegularExpression(@"^(\d|\w)+$", ErrorMessage = "No spaces and special charachters!")]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required]
        [StringLength(maximumLength: 20, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 20 characters!")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        
        [Display(Name = "Password")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Display(Name = "User Roles")]
        public string UserRole { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [RegularExpression(@"^\w+( +\w+)*$|", ErrorMessage = "Spaces can only exist beetween words")]
        [Display(Name = "First Name")]
        [StringLength(maximumLength: 50, MinimumLength = 2, ErrorMessage = "First name must be between 6 and 50 characters!")]
        [Required(ErrorMessage = "You must provide first name")]
        public string FirstName { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [RegularExpression(@"^\w+( +\w+)*$|", ErrorMessage = "Spaces can only exist beetween words")]
        [Display(Name = "Last Name")]
        [StringLength(maximumLength: 50, MinimumLength = 2, ErrorMessage = "Last name must be between 6 and 50 characters!")]
        [Required(ErrorMessage = "You must provide last name")]
        public string LastName { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [RegularExpression(@"^\w+( +\w+)*$|", ErrorMessage = "Spaces can only exist beetween words")]
        [Required(ErrorMessage = "You must provide address")]
        [Display(Name = "Address ")]
        [StringLength(maximumLength: 50, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 50 characters!")]
        public string Address { get; set; }

        [Required]
        [System.Web.Mvc.Remote("DoesEmailExist", "Users", ErrorMessage = "E-mail address already exists!")]
        [EmailAddress(ErrorMessage ="Invalid e-mail address.")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(maximumLength: 50, MinimumLength = 4, ErrorMessage = "User name must be between 2 and 50 characters!")]
        [RegularExpression(@"^(\d|\w)+$",ErrorMessage = "No spaces and special charachters!")]
        [Display(Name = "User Name")]
        [System.Web.Mvc.Remote("DoesUserNameExist", "Users", ErrorMessage = "User Name already exists!")]
        public string UserName { get; set; }

        [Required]
        [StringLength(maximumLength: 20, MinimumLength = 6, ErrorMessage = "Password must be between 5 and 20 characters!")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [StringLength(maximumLength: 20, MinimumLength = 6, ErrorMessage = "Password must be between 5 and 20 characters!")]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress(ErrorMessage = "Invalid e-mail address.")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(maximumLength: 20, MinimumLength = 6, ErrorMessage = "Password must be between 5 and 20 characters!")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [StringLength(maximumLength: 20, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 20 characters!")]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress(ErrorMessage = "Invalid e-mail address.")]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
