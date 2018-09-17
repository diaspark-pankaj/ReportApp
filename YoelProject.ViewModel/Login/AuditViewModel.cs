using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace YoelProject.Login
{

    public class AuditListViewModel
    {
        public List<AuditViewModel> AuditList { get; set; }
        public List<UserListDetailViewModel> UserList { get; set; }
        public string searchField { get; set; }
        public int NumberOfAudits { get; set; }
        public int AuditsPerPage { get; set; }
        public IEnumerable<AuditViewModel> Audits { get; set; }
    }



    public class AuditViewModel
    {
        #region Primitive Properties

        [Required]
        public Guid AuditID { get; set; }

        public string IPAddress { get; set; }

        public string UserName { get; set; }

        public string URLAccessed { get; set; }

        public DateTime TimeAccessed { get; set; }

        public string FormatedTimeAccessed
        {
            get
            {
                return TimeAccessed.ToString("MM/dd/yyyy hh:mm:ss tt");
            }
        }

        public string CustomMessage { get; set; }

        public int UserId { get; set; }

        #endregion

        #region Navigation Properties

        #endregion
    }
}
