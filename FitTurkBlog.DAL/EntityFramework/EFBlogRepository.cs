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
    public class EFBlogRepository : GenericRepository<Blog>, IBlogDAL
    {

        public List<Blog> GetListWithCategory()
        {
            using (var sqlDbContext = new SqlDbContext())
            {
                return sqlDbContext.Blogs.Include(x => x.Category).ToList();
            }
        }

        public List<Blog> GetListWithCategoryByWriter(int id)
        {
            using (var sqlDbContext = new SqlDbContext())
            {
                return sqlDbContext.Blogs.Include(x => x.Category).Where(x => x.WriterID == id).ToList();
            }
        }

    }
}
