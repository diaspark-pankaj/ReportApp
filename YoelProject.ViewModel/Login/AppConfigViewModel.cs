using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace YoelProject.ViewModel.Login
{
    public class AppConfigViewModel
    {
        [Required]
        public virtual int Id { get; set; }
        public virtual string AppKey { get; set; }

        [StringLength(200, ErrorMessage = "Maximum allowed characters upto 200")]
        [Required(ErrorMessage = "App Vaue is required")]
        public virtual string AppVaue { get; set; }
        public virtual int? ModifiedBy { get; set; }
        public List<AppConfigViewModel> AppConfigList { get; set; }
        public virtual string Caption { get; set; }
        public string Section { get; set; }
    }
}
