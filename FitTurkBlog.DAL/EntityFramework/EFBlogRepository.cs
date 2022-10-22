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

        public List<Blog> GetListWithCategoryWriter()
        {
            using (var sqlDbContext = new SqlDbContext())
            {
                return sqlDbContext.Blogs.Include(x => x.Category).Include(x => x.BlogWriter).ToList();
            }
        }

        public List<Blog> GetListWithCategoryByWriter(int id)
        {
            using (var sqlDbContext = new SqlDbContext())
            {
                return sqlDbContext.Blogs.Include(x => x.Category).Where(x => x.BlogWriterId == id).ToList();
            }
        }

        public List<Blog> GetListWithCategoryWriterByKey(int id,string key)
        {
            using (var sqlDbContext = new SqlDbContext())
            {
                return sqlDbContext.Blogs.Include(x => x.Category).Include(x => x.BlogWriter).Where(x => x.BlogWriterId == id && (x.BlogTitle.Contains(key) || x.Category.CategoryName.Contains(key))).ToList();
            }
        }



        public Blog GetListWithCategoryWriterByBlogID(int id)
        {
            using (var sqlDbContext = new SqlDbContext())
            {
                return sqlDbContext.Blogs.Include(x => x.Category).Include(x => x.BlogWriter).FirstOrDefault(x => x.BlogID == id);
            }
        }

    }
}
