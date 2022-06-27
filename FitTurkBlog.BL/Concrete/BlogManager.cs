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
    public class BlogManager : IBlogService
    {
        IBlogDAL _blogDAL;

        public BlogManager(IBlogDAL blogDAL)
        {
            _blogDAL = blogDAL;
        }

        public List<Blog> GetBlogListWithCategory()
        {
            return _blogDAL.GetListWithCategory();
        }

        public List<Blog> GetListWithCategoryByWriterBm(int id)
        {
            return _blogDAL.GetListWithCategoryByWriter(id);
        }

        public List<Blog> GetBlogListByWriter(int id)
        {
            return _blogDAL.GetListAll(x => x.WriterID == id);
        }


        public List<Blog> GetBlogByID(int id)
        {
            return _blogDAL.GetListAll(x => x.BlogID == id);
        }


        public List<Blog> BlogGetLast3Blog()
        {
            return _blogDAL.ListAll().Take(3).ToList();
        }

        public void Add(Blog t)
        {
            _blogDAL.Add(t);
        }

        public void Delete(Blog t)
        {
            _blogDAL.Delete(t);
        }

        public void Update(Blog t)
        {
            _blogDAL.Update(t);
        }

        public List<Blog> GetList()
        {
            return _blogDAL.ListAll();
        }

        public Blog TGetById(int id)
        {
            return _blogDAL.GetById(id);
        }
    }
}
