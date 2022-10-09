using System.ComponentModel.DataAnnotations;

namespace FitTurkBlog.UI.Models
{
    public class UserResetPassword
    {
        [Display(Name = "E-posta Adresi")]
        [Required(ErrorMessage = "Lütfen e-posta adresinizi giriniz !")]
        public string URPEmail { get; set; }
    }
}
