using FitTurkBlog.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTurkBlog.BL.Abstract
{
    public interface IBlogService:IGenericService<Blog>
    {
        List<Blog> GetBlogListWithCategory();
        List<Blog> GetBlogListWithCategoryWriter();
        List<Blog> GetBlogListByWriter(int id);
        Blog GetListWithCategoryWriterByBlogID(int id);
        List<Blog> GetListWithCategoryWriterByKey(int id, string key);

    }
}
