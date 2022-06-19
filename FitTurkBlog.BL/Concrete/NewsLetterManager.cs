using FitTurkBlog.BL.Abstract;
using FitTurkBlog.DAL.Abstract;
using FitTurkBlog.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTurkBlog.BL.Concrete
{
    public class NewsLetterManager : INewsLetterService
    {
        INewsLetterDAL _newsLetterDAL;

        public NewsLetterManager(INewsLetterDAL newsLetterDAL)
        {
            _newsLetterDAL = newsLetterDAL;
        }

        public void NewsLetterAdd(NewsLetter newsLetter)
        {
            _newsLetterDAL.Add(newsLetter);
        }

    }
}
