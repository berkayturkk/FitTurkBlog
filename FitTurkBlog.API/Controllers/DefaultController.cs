using FitTurkBlog.DAL.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace FitTurkBlog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DefaultController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetWriterList()
        {
            using var c = new SqlDbContext();
            var values = c.Writers.ToList();
            return Ok(values);
        }

        //    [HttpPost]
        //    public IActionResult AddWriter(WriterClass writer)
        //    {
        //        writers.Add(writer);
        //        return Ok(writers);
        //    }

        //    [HttpGet("{id}")]

        //    public IActionResult GetWriterById(int id)
        //    {
        //        var writer = writers.Find(x => x.Id == id);
        //        return Ok(writer);
        //    }

        //    [HttpDelete("{id}")]
        //    public IActionResult DeleteWriter(int id)
        //    {
        //        var writer = writers.Find(x => x.Id == id);
        //        var delwriter = writers.Remove(writer);
        //        return Ok(writers);
        //    }

        //    [HttpPut]
        //    public IActionResult UpdateWriter(WriterClass writer)
        //    {
        //        var upwriter = writers.FirstOrDefault(x => x.Id == writer.Id);
        //        upwriter.Name = writer.Name;
        //        return Ok(writers);
        //    }

        //    public static List<WriterClass> writers = new List<WriterClass>
        //    {
        //        new WriterClass
        //        {
        //            Id = 1,
        //            Name = "Harun"
        //        },
        //        new WriterClass
        //        {
        //            Id = 2,
        //            Name = "Bekir"
        //        },
        //        new WriterClass
        //        {
        //            Id = 3,
        //            Name = "Talih"
        //        }
        //    };
        //}
    }
}
