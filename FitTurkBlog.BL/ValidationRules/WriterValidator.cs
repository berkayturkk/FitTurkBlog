using FitTurkBlog.Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FitTurkBlog.BL.ValidationRules
{
    public class WriterValidator : AbstractValidator<Writer>
    {
        public WriterValidator()
        {
            RuleFor(x => x.WriterName).NotEmpty().WithMessage("Ad-soyad alanı boş geçilemez !");
            RuleFor(x => x.WriterName).Length(2, 50).WithMessage("Ad-soyad alanı en az 2, en fazla 50 karakter içermelidir !");
            RuleFor(x => x.WriterMail).NotEmpty().WithMessage("E-posta adresi alanı boş geçilemez !");
            RuleFor(x => x.WriterMail).EmailAddress().WithMessage("E-posta adresi uygun formda değil !");
            RuleFor(x => x.WriterPassword).NotEmpty().WithMessage("Şifre alanı boş geçilemez!");
            RuleFor(x => x.WriterConfirmPassword).NotEmpty().WithMessage("Şifre alanı boş geçilemez!");
            RuleFor(x => x.WriterConfirmPassword).Equal(x => x.WriterPassword).WithMessage("Şifre onay alanı şifre alanı ile uyuşmuyor !");
        }
    }
}
