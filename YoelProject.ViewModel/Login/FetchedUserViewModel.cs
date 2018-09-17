
using System;
using System.Collections.Generic;

namespace YoelProject.Login
{
    public class FetchedUserViewModel
    {
        public int UserId { get; set; }

        public string FullName { get; set; }      

        public string ThumbImage { get; set; }

        public string Email { get; set; }
        public string UserName { get; set; }
       
        public int RoleId { get; set; }

        public List<int> RoleLists { get; set; }

        public string OrganizationName { get; set; }
      
        public int Status { get; set; }

        public DateTime LastPasswordChangeDate { get; set; }

        public int? FailedLoginAttempts { get; set; }

        public DateTime? lockedDate { get; set; }
      
        public List<int> BusinessUnitIds { get; set; }

	

        public List<int> MatterTypeIds { get; set; }

		

		public int UserTypeId { get; set; }
        public int OfficeId { get; set; }
        public string SessionId { get; set; }

        public bool IsActive { get; set; }

        public DateTime? DateModified { get; set; }

        public FetchedUserProfileViewModel FetchedUserProfileViewModel { get; set; }

        public string PasswordHash { get; set; }
        public string LawFirm { get; set; }

		public int LawFirm_ID { get; set; }

		

        public int DefaultDashboardID { get; set; }

    }

    public class FetchedUserProfileViewModel
    {
        public int Id { get; set; }

        public int SecretQuestionId { get; set; }

        public string SecretAnswer { get; set; }

        public string Title { get; set; }

        public string FullName { get; set; }

        public string PreferredName { get; set; }

        public string ProfileImage { get; set; }        

        public string EmailAlt { get; set; }       
        public string AboutMe { get; set; }            
        public string PhoneNo { get; set; }
        public DateTime? DateModified { get; set; }
    }


    public class BasicuserInfo
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Id { get; set; }
        public string Email { get; set; }
    }

	
}
