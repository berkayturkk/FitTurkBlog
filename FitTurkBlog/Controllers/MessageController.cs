using FitTurkBlog.BL.Concrete;
using FitTurkBlog.DAL.Context;
using FitTurkBlog.DAL.EntityFramework;
using FitTurkBlog.Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace FitTurkBlog.UI.Controllers
{
    [AllowAnonymous]
    public class MessageController : Controller
    {
        Message2Manager _message2Manager = new Message2Manager(new EFMessage2Repository());
        SqlDbContext _sqlDbContext = new SqlDbContext();
        public IActionResult InBox()
        {
            var userName = User.Identity.Name;
            var userMail = _sqlDbContext.Users.Where(x => x.UserName == userName).Select(y => y.Email).FirstOrDefault();
            var writerID = _sqlDbContext.Users.Where(x => x.Email == userMail).Select(y => y.Id).FirstOrDefault();
            var values = _message2Manager.GetInBoxListByWriter(writerID);
            return View(values);
        }

        public IActionResult MessageDetails(int id)
        {
            var value = _message2Manager.TGetById(id);
            return View(value);
        }

        [HttpGet]
        public IActionResult SendMessage()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SendMessage(Message2 message2)
        {
            var userName = User.Identity.Name;
            var userMail = _sqlDbContext.Users.Where(x => x.UserName == userName).Select(y => y.Email).FirstOrDefault();
            var writerID = _sqlDbContext.Users.Where(x => x.Email == userMail).Select(y => y.Id).FirstOrDefault();
            message2.MessageSenderID = writerID;
            message2.MessageReceiverID = 3;
            message2.MessageStatus = true;
            message2.MessageDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
           if(message2.MessageSenderID != message2.MessageReceiverID)
            {
                _message2Manager.Add(message2);
            }
            return RedirectToAction("InBox", "Message");
        }

        public IActionResult DeleteMessage(int id)
        {
            var messageValue = _message2Manager.TGetById(id);
            _message2Manager.Delete(messageValue);
            return RedirectToAction("InBox", "Message");
        }
    }
}
