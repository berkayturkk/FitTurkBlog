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
    public class UserManager : IUserService
    {
        IUserDAL _userDAL;

        public UserManager(IUserDAL userDAL)
        {
            _userDAL = userDAL;
        }

        public void Add(AppUser t)
        {
            throw new NotImplementedException();
        }

        public void Delete(AppUser t)
        {
            throw new NotImplementedException();
        }

        public List<AppUser> GetList()
        {
            return _userDAL.ListAll();
        }


        public AppUser TGetById(int id)
        {
            return _userDAL.GetById(id);
        }

        public void Update(AppUser t)
        {
            throw new NotImplementedException();
        }

        public List<AppUser> GetListAllById(int id)
        {
            return _userDAL.GetListAll(x => x.Id == id);
        }
    }
}
