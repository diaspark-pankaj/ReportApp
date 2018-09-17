using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoelProject.ViewModel.Admin
{
   public class UserViewModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string OriginalImage { get; set; }
        public string ThumbImage { get; set; }
        public int EntityId { get; set; }
        public System.DateTime LastPasswordChangeDate { get; set; }
        public Nullable<int> FailedLoginAttempts { get; set; }
        public Nullable<System.DateTime> LockedDate { get; set; }
        public int Status { get; set; }
        public bool IsActive { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string PasswordHash { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public int ModifiedBy { get; set; }
    }
}
