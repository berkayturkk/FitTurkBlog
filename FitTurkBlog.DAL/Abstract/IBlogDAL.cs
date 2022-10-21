using FitTurkBlog.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTurkBlog.DAL.Abstract
{
    public interface IBlogDAL : IGenericDAL<Blog>
    {
        List<Blog> GetListWithCategory();
        List<Blog> GetListWithCategoryWriter();
        List<Blog> GetListWithCategoryByWriter(int id);
        Blog GetListWithCategoryWriterByBlogID(int id);

    }
}
