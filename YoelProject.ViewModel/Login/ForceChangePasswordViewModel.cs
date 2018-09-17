using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace YoelProject.ViewModel.Login
{
    public class ForceChangePasswordViewModel
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Please enter Old Password")]
        [Display(Name = "Old Password")]
        [RegularExpression("(?=^.{6,}$)(?=.*\\d)(?=.*[!@#$%^&*]+)(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$", ErrorMessage = "Password must be 6-14 characters in length containing at least one Uppercase, one Lowercase, one Number and one Special Letter.")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Please enter New Password")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "The {0} must be 6-14 characters long.", MinimumLength = 6)]
        [Display(Name = "New Password")]
        [RegularExpression("(?=^.{6,}$)(?=.*\\d)(?=.*[!@#$%^&*]+)(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$", ErrorMessage = "Password must be 6-14 characters in length containing at least one Uppercase, one Lowercase, one Number and one Special Letter.")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Please enter Confirm Password")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "The {0} must be 6-14 characters long.", MinimumLength = 6)]
        [Display(Name = "Confirm Password")]
        [System.ComponentModel.DataAnnotations.Compare("NewPassword", ErrorMessage = "Passwords do not match")]
        [RegularExpression("(?=^.{6,}$)(?=.*\\d)(?=.*[!@#$%^&*]+)(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$", ErrorMessage = "Password must be 6-14 characters in length containing at least one Uppercase, one Lowercase, one Number and one Special Letter.")]
        public string ConfirmPassword { get; set; }

        public bool IsPwdExpire { get; set; }

        public string SecurityStamp { get; set; }

        public string UserName { get; set; }
      
    }
}
