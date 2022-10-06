using FitTurkBlog.DAL.Context;
using FitTurkBlog.Entities.Concrete;
using FitTurkBlog.UI.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FitTurkBlog.UI.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Index(Writer writer)
        {
            SqlDbContext sqlContext = new SqlDbContext();
            var dataValue = sqlContext.Writers.FirstOrDefault(x => x.WriterMail == writer.WriterMail && x.WriterPassword == writer.WriterPassword);
            if (dataValue != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,writer.WriterMail)
                };
                var userIdentity = new ClaimsIdentity(claims, "a");
                ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                await HttpContext.SignInAsync(principal);

                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                return View();
            }

        }
    }
}

