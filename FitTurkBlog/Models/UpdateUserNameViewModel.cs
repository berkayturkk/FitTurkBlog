using System.ComponentModel.DataAnnotations;

namespace FitTurkBlog.UI.Models
{
    public class UpdateUserNameViewModel
    {
        [Display(Name = "Kullanıcı Adı")]
        [Required(ErrorMessage = "Lütfen kullanıcı adınızı giriniz !")]
        public string UpUserName { get; set; }
    }
}
