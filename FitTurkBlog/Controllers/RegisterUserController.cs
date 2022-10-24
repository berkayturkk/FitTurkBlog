using FitTurkBlog.BL.Concrete;
using FitTurkBlog.DAL.EntityFramework;
using FitTurkBlog.Entities.Concrete;
using FitTurkBlog.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace FitTurkBlog.UI.Controllers
{
    [AllowAnonymous]
    public class RegisterUserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        Message2Manager messageManager = new Message2Manager(new EFMessage2Repository());
        NotificationManager notificationManager = new NotificationManager(new EFNotificationRepository());

        public RegisterUserController(UserManager<AppUser> userManager)
        {
            this._userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new UserSignUpViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Index(UserSignUpViewModel p)
        {

            if (ModelState.IsValid)
            {
                if(p.IsAcceptContract == true)
                {
                    AppUser user = new AppUser()
                    {
                        Email = p.Mail,
                        UserName = p.UserName,
                        NameSurname = p.NameUsername,
                        ImageUrl = "/FitTurkBlog/newImages/eedebfdc-b507-4a98-b5c3-2f9b37a1a32a.jpg",
                        Status = true
                    };

                    var result = await _userManager.CreateAsync(user, p.Password);

                    if (result.Succeeded)
                    {
                        var sender = await _userManager.FindByNameAsync(p.UserName);

                        Notification notification = new Notification
                        {
                            NotificationDate = DateTime.Now,
                            NotificationStatus = true,
                            NotificationType = "Yeni Kayıt",
                            NotificationTypeSymbol = "mdi mdi-account-plus",
                            NotificationTypeSymbolColor = "preview-icon bg-success",
                            NotificationDetails = sender.NameSurname + " FitTurkBlog sitemize kayıt oldu. Rol ataması gerçekleştirilecek !"
                        };

                        notificationManager.Add(notification);

                        SmtpClient client = new SmtpClient();
                        client.Credentials = new NetworkCredential("fitturkblog@gmail.com", "mehblijlpxajxrrl");
                        client.Port = 587;
                        client.Host = "smtp.gmail.com";
                        client.EnableSsl = true;

                        MailMessage mail = new MailMessage();
                        mail.To.Add(sender.Email);
                        mail.From = new MailAddress("fitturkblog@gmail.com", "FITTURKBLOG");
                        mail.IsBodyHtml = true;
                        mail.Subject = "Kayıt Olma";
                        mail.Body += "<h3>Merhaba " + sender.NameSurname + ",</h3>" + "<p>FitTurkBlog sitesine kayıt talebiniz ekibimiz tarafına ulaşmıştır.</p>" + "<p>Ekibimiz en kısa sürede hesabınızı aktifleştirip size mail yoluyla bilgilendirecektir.</p><p>FitTurkBlog sitesinde sizleri de görmekten mutluluk duyduk. Hoşgeldiniz.</p><p>FitTürkBlog Ekibi</p>";

                        client.Send(mail);

                        return RedirectToAction("Index", "LoginUser");
                    }
                    else
                    {
                        foreach (var item in result.Errors)
                        {
                            ModelState.AddModelError("", item.Description);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("IsAcceptContract","Lütfen kullanım şartlarını kabul ediniz !");
                    return View(p);
                }
            }

            return View(p);
        }
    }
}
