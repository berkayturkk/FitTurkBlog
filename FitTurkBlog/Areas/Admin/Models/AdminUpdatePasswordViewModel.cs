using System.ComponentModel.DataAnnotations;

namespace FitTurkBlog.UI.Areas.Admin.Models
{
    public class AdminUpdatePasswordViewModel
    {
        [Display(Name = "Yeni Şifre")]
        [Required(ErrorMessage = "Lütfen yeni şifrenizi giriniz !")]
        public string NewPassword { get; set; }

        [Display(Name = "Yeni Şifre Tekrarı")]
        [Compare("NewPassword", ErrorMessage = "Yeni şifre ile yeni şifre tekrarı uyuşmuyor !")]
        public string NewConfirmPassword { get; set; }
    }
}
