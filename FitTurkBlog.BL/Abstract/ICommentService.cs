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
        //void CommentAdd(Comment comment);
        //void CommentUpdate(Comment comment);
        List<Comment> GetListAllComment();
        List<Comment> CommentGetList(int id);

    }
}
