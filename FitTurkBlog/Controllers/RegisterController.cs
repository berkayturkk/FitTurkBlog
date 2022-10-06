using FitTurkBlog.BL.Concrete;
using FitTurkBlog.BL.ValidationRules;
using FitTurkBlog.DAL.EntityFramework;
using FitTurkBlog.Entities.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FitTurkBlog.UI.Controllers
{
    public class RegisterController : Controller
    {
        WriterManager writerManager = new WriterManager(new EFWriterRepository());

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Index(Writer writer)
        {
            WriterValidator writerValidationRules = new WriterValidator();
            ValidationResult results = writerValidationRules.Validate(writer);
            if (results.IsValid)
            {
                writer.WriterStatus = true;
                writer.WriterAbout = "Lorem ipsum dolor sit amet consectetur adipisicing elit. Quo modi error facere labore temporibus quae ad, nemo dolorem ducimus nisi. Perferendis velit aspernatur non impedit architecto nisi recusandae, ratione in sapiente beatae corporis delectus eaque, culpa possimus cupiditate molestiae. Neque libero autem doloremque hic deserunt fugiat ab incidunt. Id sequi dignissimos ipsa asperiores esse neque. Laborum saepe voluptatibus dolorem. Nisi quam laboriosam eos minus molestiae sint natus ut corrupti vitae repellat deleniti, veniam, nam incidunt dolorem voluptates facilis nemo voluptatum architecto doloribus assumenda dolore eum est aliquam? Ipsam quod ea, repellendus provident, veritatis repudiandae enim mollitia saepe autem accusamus quo aut quasi dicta consequuntur! Odio sapiente dolores explicabo culpa, eos dignissimos cupiditate ducimus porro delectus officia ea eum? Quia, necessitatibus! Eos voluptatum maiores quasi repellat? Assumenda praesentium quidem accusantium officiis inventore voluptates illo! Commodi obcaecati blanditiis aperiam! Accusantium hic labore autem sapiente, cum quas recusandae quae? Libero nihil quidem fugit in! Ratione aliquam enim corporis facilis labore incidunt soluta? A aliquam perspiciatis quasi accusantium illo ullam ad ea quae perferendis amet, eos similique nemo? Sequi voluptas autem sunt minus labore harum doloremque molestiae repudiandae, magnam asperiores, recusandae ut placeat at quaerat laboriosam porro, blanditiis error facilis non est modi repellat!";
                writer.WriterImage = "/FitTurkBlog/images/team2.jpg";
                writerManager.Add(writer);
                
                return RedirectToAction("Index", "Login");
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();

        }
    }
}
