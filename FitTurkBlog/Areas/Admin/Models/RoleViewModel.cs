using System.ComponentModel.DataAnnotations;

namespace FitTurkBlog.UI.Areas.Admin.Models
{
    public class RoleViewModel
    {
        [Required(ErrorMessage ="Lütfen rol adı giriniz !")]
        public string RoleName { get; set; }
    }
}
