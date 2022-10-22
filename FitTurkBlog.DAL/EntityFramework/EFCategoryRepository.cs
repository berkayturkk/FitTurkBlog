using FitTurkBlog.DAL.Abstract;
using FitTurkBlog.DAL.Context;
using FitTurkBlog.DAL.Repositories;
using FitTurkBlog.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTurkBlog.DAL.EntityFramework
{
    public class EFCategoryRepository : GenericRepository<Category>, ICategoryDAL
    {
        public List<Category> GetListCategoryByKey(string key)
        {
            using (var sqlDbContext = new SqlDbContext())
            {
                return sqlDbContext.Categories.Where(x => x.CategoryName.Contains(key)).ToList();
            }
        }
    }
}
