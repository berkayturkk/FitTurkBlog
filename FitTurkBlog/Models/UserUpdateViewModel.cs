using System.ComponentModel.DataAnnotations;

namespace FitTurkBlog.UI.Models
{
    public class UserUpdateViewModel
    {
        [Display(Name = "Ad Soyad")]
        [Required(ErrorMessage = "Lütfen ad soyad giriniz !")]
        public string UpNameSurname { get; set; }

        [Display(Name = "E-posta Adresi")]
        [Required(ErrorMessage = "Lütfen e-posta adresinizi giriniz !")]
        public string UpMail { get; set; }

        [Display(Name = "Görsel Yolu")]
        [Required(ErrorMessage = "Lütfen görsel ekleyiniz !")]
        public string UpImageUrl { get; set; }

        [Display(Name = "Yazar Hakkında")]
        [Required(ErrorMessage = "Lütfen yazar hakkında giriniz !")]
        public string UpAbout { get; set; }

    }
}
