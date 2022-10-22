using FitTurkBlog.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTurkBlog.DAL.Abstract
{
    public interface IUserDAL : IGenericDAL<AppUser>
    {
        List<AppUser> GetListWriterByKey(string key);
    }
}
