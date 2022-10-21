using FitTurkBlog.BL.Concrete;
using FitTurkBlog.DAL.EntityFramework;
using FitTurkBlog.Entities.Concrete;
using FitTurkBlog.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FitTurkBlog.UI.Controllers
{
    [AllowAnonymous]
    public class RegisterUserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        Message2Manager messageManager = new Message2Manager(new EFMessage2Repository());

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

                        Message2 message = new Message2
                        {
                            MessageSenderID = sender.Id,
                            MessageReceiverID = 1,
                            MessageSubject = "Yeni Kayıt",
                            MessageDetails = "Merhaba, adım " + user.NameSurname + " FitTurkBlog sitenize yeni kayıt oldum. Herhangi bir rol atamam olmadığı için blog yazamıyorum. Rolümü yazar olarak güncellemenizi rica ediyorum. Şimdiden teşekkür ederim. İyi günler.",
                            MessageDate = DateTime.Now,
                            MessageStatus = true
                        };

                        messageManager.Add(message);

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
