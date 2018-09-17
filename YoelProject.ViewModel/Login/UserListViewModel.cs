using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace YoelProject.Login
{
    public class UserListViewModel
    {
        public UserListViewModel()
        {
            RustyData = new Dictionary<string, object>();
        }
        public List<UserListDetailViewModel> UsersList { get; set; }
        public String SearchField { get; set; }
        public Dictionary<string, object> RustyData;
        public int RecordCount { get; set; }

    }

    public class UserListDetailViewModel
    {
        public int id { get; set; }

        public String UserName { get; set; }
        public String Email { get; set; }
        public String FullName { get; set; }
        public String OfficeName { get; set; }
        public String UserType { get; set; }
        public int EntityId { get; set; }
        public int ? OfficeId { get; set; }
        public bool IsActive { get; set; }
        [Required(ErrorMessage = "Please enter verification code")]
        public string Verification_Code { get; set; }
        public bool IsCodeExpired { get; set; }
        public int Expiration_Time { get; set; }
        public string DefaultDashboardUrl { get; set; }

        public string PortalType {get;set;}
        public string rememberMe {get;set;}
        public string userPwd {get;set;}
        public string sessionId {get;set;}
    }
}
