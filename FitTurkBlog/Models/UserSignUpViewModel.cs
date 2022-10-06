using System.ComponentModel.DataAnnotations;

namespace FitTurkBlog.UI.Models
{
    public class UserSignUpViewModel
    {
        [Display(Name = "Ad Soyad")]
        [Required(ErrorMessage ="Lütfen ad soyad giriniz !")]
        public string NameUsername { get; set; }

        [Display(Name = "Şifre")]
        [Required(ErrorMessage = "Lütfen şifre giriniz !")]
        public string Password { get; set; }

        [Display(Name = "Şifre Tekrarı")]
        [Compare("Password",ErrorMessage ="Şifreler uyuşmuyor !")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Mail Adresi")]
        [Required(ErrorMessage = "Lütfen mail adresi giriniz !")]
        public string Mail { get; set; }

        [Display(Name = "Kullanıcı Adı")]
        [Required(ErrorMessage = "Lütfen kullanıcı adınızı giriniz !")]
        public string UserName { get; set; }

        [Display(Name = "Kullanım Şartları")]
        [Required(ErrorMessage = "Lütfen kullanım şartlarını kabul ediniz !")]
        public bool IsAcceptContract { get; set; }
    }
}
