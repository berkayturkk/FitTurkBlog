using FitTurkBlog.Entities.Concrete;
using FitTurkBlog.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FitTurkBlog.UI.Controllers
{
    [AllowAnonymous]
    public class RegisterUserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

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
                        NameSurname = p.NameUsername
                    };

                    var result = await _userManager.CreateAsync(user, p.Password);

                    if (result.Succeeded)
                    {
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
