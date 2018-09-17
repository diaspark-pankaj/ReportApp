
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace YoelProject.ViewModel.Login
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "Please enter email")]
        [EmailAddress(ErrorMessage = "Please enter valid email")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter username")]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        public string BaseUrl { get; set; }

        public bool isEmailSent { get; set; }

        //public int UserId { get; set; }
        //[Required(ErrorMessage = "Please enter your Username")]
        ////[RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Invalid email address")]
        ////[DataType(DataType.EmailAddress)]
        ////[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.")]
        //[Remote("EmailExist", "Login", ErrorMessage = "User name is not avilable.")]
        //public string UserName { get; set; }
        //[Required(ErrorMessage = "Please Select Security Question")]
        //public int SecurityQuestionId { get; set; }
        //[Required(ErrorMessage = "Please enter your Security Answer")]
        //public string SecurityAnswer { get; set; }

        //public List<SelectListItem> SecretQuestionList { get; set; }

    }
}
