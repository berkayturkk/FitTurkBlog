using FitTurkBlog.Entities.Concrete;
using FitTurkBlog.UI.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace FitTurkBlog.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class AdminRoleController : Controller
    {
        private readonly RoleManager<AppRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;

        public AdminRoleController(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var values = _roleManager.Roles.ToList();
            return View(values);
        }

        [HttpGet]
        public IActionResult AddRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddRole(RoleViewModel model)
        {
            if(ModelState.IsValid)
            {
                AppRole role = new AppRole
                {
                    Name = model.RoleName
                };

                var result = await _roleManager.CreateAsync(role);

                if(result.Succeeded)
                {
                    return RedirectToAction("Index", "AdminRole");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("",item.Description);
                    }
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult EditRole(int id)
        {
            var value = _roleManager.Roles.FirstOrDefault(x => x.Id == id);
            RoleUpdateViewModel model = new RoleUpdateViewModel
            {
                ID = value.Id,
                Name = value.Name
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditRole(RoleUpdateViewModel model)
        {
            var value = _roleManager.Roles.Where(x => x.Id == model.ID).FirstOrDefault();
            value.Name = model.Name;
            var result = await _roleManager.UpdateAsync(value);
            if(result.Succeeded)
            {
                return RedirectToAction("Index", "AdminRole");
            }
            return View(value);
        }

        public async Task<IActionResult> DeleteRole(int id)
        {
            var value = _roleManager.Roles.FirstOrDefault(x => x.Id == id);
            var result = await _roleManager.DeleteAsync(value);
            if(result.Succeeded)
            {
                return RedirectToAction("Index", "AdminRole");
            }
            return View();
        }

        public IActionResult UserRoleList()
        {
            var values = _userManager.Users.ToList();
            return View(values);
        }

        [HttpGet]
        public async Task<IActionResult> AssignRole(int id)
        {
            var user = _userManager.Users.FirstOrDefault(x => x.Id == id);
            var roles = _roleManager.Roles.ToList();

            TempData["UserId"] = user.Id;

            var userRoles = await _userManager.GetRolesAsync(user);

            List<RoleAssignViewModel> models = new List<RoleAssignViewModel>();
            foreach (var item in roles)
            {
                RoleAssignViewModel _model = new RoleAssignViewModel();
                _model.RoleId = item.Id;
                _model.RoleName = item.Name;
                _model.Exists = userRoles.Contains(item.Name);
                models.Add(_model);
            }

            return View(models);

        }

        [HttpPost]
        public async Task<IActionResult> AssignRole(List<RoleAssignViewModel> models)
        {
            var userId = (int)TempData["UserId"];
            var user = _userManager.Users.FirstOrDefault(x => x.Id == userId);

            foreach (var item in models)
            {
                if(item.Exists)
                {
                    SmtpClient client = new SmtpClient();
                    client.Credentials = new NetworkCredential("fitturkblog@gmail.com", "mehblijlpxajxrrl");
                    client.Port = 587;
                    client.Host = "smtp.gmail.com";
                    client.EnableSsl = true;

                    MailMessage mail = new MailMessage();
                    mail.To.Add(user.Email);
                    mail.From = new MailAddress("fitturkblog@gmail.com", "FITTURKBLOG");
                    mail.IsBodyHtml = true;
                    mail.Subject = "Hesap Aktifleştirme";
                    mail.Body += "<h3>Merhaba " + user.NameSurname + ",</h3>" + "<p>Kayıt olma talebinizi gerçekleştirdik. Artık hesabınıza kullanıcı adı ve şifreniz ile giriş sağlayabilirsiniz.</p><p>Aramıza hoşgeldiniz.</p><p>FitTurkBlog Ekibi</p>";

                    client.Send(mail);

                    await _userManager.AddToRoleAsync(user, item.RoleName);
                }
                else
                {
                    await _userManager.RemoveFromRoleAsync(user, item.RoleName);
                }
            }
            return RedirectToAction("UserRoleList", "AdminRole");
        }



    }
}
