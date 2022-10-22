using FitTurkBlog.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTurkBlog.BL.Abstract
{
    public interface ICommentService
    {
        List<Comment> GetListAllComment();
        List<Comment> GetListAllCommentByWriterWithBlog(int id);
        List<Comment> CommentGetList(int id);
        List<Comment> GetListCommentByKey(string key);

    }
}
