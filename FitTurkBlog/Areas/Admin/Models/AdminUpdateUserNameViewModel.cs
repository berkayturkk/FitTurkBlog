using System.ComponentModel.DataAnnotations;

namespace FitTurkBlog.UI.Areas.Admin.Models
{
    public class AdminUpdateUserNameViewModel
    {
        [Display(Name = "Kullanıcı Adı")]
        [Required(ErrorMessage = "Lütfen kullanıcı adınızı giriniz !")]
        public string UpUserName { get; set; }
    }
}
