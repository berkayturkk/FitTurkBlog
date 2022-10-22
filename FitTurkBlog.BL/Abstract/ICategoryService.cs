using FitTurkBlog.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTurkBlog.BL.Abstract
{
    public interface ICategoryService:IGenericService<Category>
    {
        List<Category> GetListCategoryByKey(string key);
    }
}
