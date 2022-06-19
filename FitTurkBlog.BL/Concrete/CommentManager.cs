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
    public class CommentManager : ICommentService
    {
        ICommentDAL _commentDAL;

        public CommentManager(ICommentDAL commentDAL)
        {
            _commentDAL = commentDAL;
        }

        public void CommentAdd(Comment comment)
        {
            _commentDAL.Add(comment);   
        }

        public List<Comment> CommentGetList(int id)
        {
            return _commentDAL.GetListAll(x => x.BlogID == id);
        }

    }
}
