
using System.ComponentModel.DataAnnotations;

namespace Capstone.Web
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "This field is required.")]
        [Display(Name = "Email:")]
        public string Email { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        [Display(Name = "Password:")]
        public string Password { get; set; }

        public bool IsValid
        {
            get
            {
                //bool result = false;
                //if (!result && !Email.Contains("@") && !Email.Contains(".com"))
                //{
                //    result = false;
                //}
                //else if (!result && Password.Length >= 8)
                //{
                //    result = false;
                //}
                //else
                //{
                //    result = true;
                //}
                //return result;
                return true;
            }
        }
    }
}