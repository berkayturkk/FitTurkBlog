using FitTurkBlog.DAL.Abstract;
using FitTurkBlog.DAL.Context;
using FitTurkBlog.DAL.Repositories;
using FitTurkBlog.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTurkBlog.DAL.EntityFramework
{
    public class EFUserRepository : GenericRepository<AppUser>, IUserDAL
    {
        public List<AppUser> GetListWriterByKey(string key)
        {
            using (var sqlDbContext = new SqlDbContext())
            {
                return sqlDbContext.Users.Where(x => x.NameSurname.Contains(key) || x.UserName.Contains(key) || x.Email.Contains(key)).ToList();
            }
        }
    }
}
