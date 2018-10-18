
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using System.Web.Mvc;

namespace Capstone.Web
{
    public class SignUpViewModel
    {
        public int RoleId { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "First Name:")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Last Name:")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Email:")]
        public string Email { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Password:")]
        //[Password]
        public string Password { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Passwords do not match")]
        [Display(Name = "Confirm Password:")]
        public string ConfirmPassword { get; set; }
        
        
        public bool IsValid
        {
            get
            {
                bool result = true;
                //if(!result && FirstName.Length <= 2) // EVAN TURNED OFF SERVER VALIDATION FOR USERS REGISTRATION FOR TESTING
                //{
                //    result = false;
                //}
                //else if(!result && LastName.Length <= 2)
                //{
                //    result = false;
                //}
                //else if(!result && !Email.Contains("@") && !Email.Contains(".com"))
                //{
                //    result = false;
                //}
                //else if(!result && Password.Length >= 8)
                //{
                //    result = false;
                //}
                //else if(!result && ConfirmPassword != Password)
                //{
                //    result = false;
                //}
                //else
                //{
                //    result = true;
                //}

                return result;
            }
        }
    }
    
}