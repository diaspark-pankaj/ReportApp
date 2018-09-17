//using Exigent.Common.Enums;
//using Exigent.ViewModels.Admin;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace YoelProject.ViewModel.Login
{
    public class LoginViewModel
    {
        //[Required(ErrorMessage = "Please enter your Username")]
        //[RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Invalid email address")]
        //[DataType(DataType.EmailAddress)]
        //[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.")]
        [Display(Name = "User Name")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Please enter your Username")]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [AllowHtml]
        [Required(ErrorMessage = "Please enter your Password")]
        [Display(Name = "Password")]
        public string Password { get; set; }

        public string ReturnAction { get; set; }

        public string ReturnController { get; set; }

        public string RedirectURL { get; set; }

        public string Msg { get; set; }
        public string SiteKeyword { get; set; }

        public string PasswordHash { get; set; }

        public bool RememberMe { get; set; }

     
        public ManageLoginPageViewModel ManageLoginPageVM { get; set; }

        //public List<KeyValuePair<int, string>> portalTypes
        //{
        //    get
        //    {
        //        return new List<KeyValuePair<int, string>>() {
        //         new KeyValuePair<int,string>(301,"Vendor Portal"),
        //         new KeyValuePair<int,string>(302,"Legal Matter Management"),
        //        };
        //    }
        //}

    }
    public class ManageLoginPageViewModel
    {
        public int Id { get; set; }
        public string File { get; set; }
        public string NotificationBoardMessage { get; set; }
        public Nullable<System.DateTime> Created { get; set; }
    }
}
