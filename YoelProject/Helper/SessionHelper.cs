using System;
using System.Collections.Generic;
using System.Data;

namespace YoelProject.Helpers
{
    public class SessionHelper
    {
        public LoggedUserInfo LoggedUserInfo { get; set; }

        public SessionHelper()
        {
            LoggedUserInfo = new LoggedUserInfo();
        }
    }

    public class LoggedUserInfo
    {
        public int UserId { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }
        public string UserName { get; set; }

        public string SecurityStamp { get; set; }

        public string ThumbImage { get; set; }

        public DateTime? DateModified { get; set; }    

        public string SessionId { get; set; }
        
        public string ExceptionMessage { get; set; }

        public string HeaderType { get; set; }

        public string DefaultDashboardUrl { get; set; }
        public string LawFirm { get; set; }
		public int LawFirm_ID { get; set; }
        public int SelectedDashboardID { get; set; }    
        public int UserTypeId { get; set; }
    }
}