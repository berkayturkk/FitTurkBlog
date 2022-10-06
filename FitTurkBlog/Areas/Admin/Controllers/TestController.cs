using FitTurkBlog.BL.Concrete;
using FitTurkBlog.DAL.Context;
using FitTurkBlog.DAL.EntityFramework;
using FitTurkBlog.Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using X.PagedList;

namespace FitTurkBlog.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AllowAnonymous]
    public class TestController : Controller
    {
        //WriterManager writerManager = new WriterManager(new EFWriterRepository());
        //private readonly SqlDbContext db;

        //public TestController(SqlDbContext db)
        //{
        //    this.db = db;
        //}

        //public IActionResult Index()
        //{
        //    var values = db.Writers.ToList().OrderByDescending(x => x.WriterID);
        //    return  View();
        //}


    }
}
