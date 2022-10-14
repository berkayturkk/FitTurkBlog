using FitTurkBlog.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTurkBlog.DAL.Abstract
{
    public interface ICommentDAL : IGenericDAL<Comment>
    {
        List<Comment> GetListComment();
    }
}
