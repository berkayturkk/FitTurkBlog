
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
    public class CategoryManager : ICategoryService
    {
        ICategoryDAL _categoryDAL;

        public CategoryManager(ICategoryDAL categoryDAL)
        {
            _categoryDAL = categoryDAL;
        }

        public void Add(Category t)
        {
            _categoryDAL.Add(t);
        }

        public void Delete(Category t)
        {
            _categoryDAL.Delete(t); 
        }

        public Category TGetById(int id)
        {
            return _categoryDAL.GetById(id);
        }

        public List<Category> GetList()
        {
            return _categoryDAL.ListAll();
        }

        public void Update(Category t)
        {
            _categoryDAL.Update(t); 
        }
    }
}
