using Microsoft.AspNetCore.Mvc;
using FitTurkBlog.BL.Concrete;
using FitTurkBlog.DAL.EntityFramework;
using FitTurkBlog.Entities.Concrete;
using System;

namespace FitTurkBlog.UI.Controllers
{
    public class ContactController : Controller
    {
        ContactManager contactManager = new ContactManager(new EFContactRepository());

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Contact contact)
        {
            contact.ContactDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            contact.ContactStatus = true;
            contact.ContactAdress = "ÜMRANİYE, İSTANBUL, TÜRKİYE";
            contact.ContactPhone = "+90 216 412 7884";
            contactManager.ContactAdd(contact);
            return RedirectToAction("Index", "Blog");
        }
    }
}
