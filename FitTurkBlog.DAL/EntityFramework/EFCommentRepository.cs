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
    public class EFCommentRepository : GenericRepository<Comment>, ICommentDAL
    {
        SqlDbContext sqlDbContext = new SqlDbContext();
        public List<Comment> GetListComment()
        {
            using (var sqlDbContext = new SqlDbContext())
            {
                return sqlDbContext.Comments.Include(x => x.Blog).ToList();
            }
        }

        public List<Comment> GetListCommentByWriter(int id)
        {
            using (var sqlDbContext = new SqlDbContext())
            {
                var username = sqlDbContext.Users.Where(x => x.Id == id).Select(x => x.NameSurname).FirstOrDefault();
                return sqlDbContext.Comments.Include(x => x.Blog).Where(x => x.CommentUserName == username).ToList();
            }
        }
        public List<Comment> GetListCommentByKey(string key)
        {
            using (var sqlDbContext = new SqlDbContext())
            {
                return sqlDbContext.Comments.Include(x => x.Blog).Where(x => x.CommentTitle.Contains(key) || x.CommentUserName.Contains(key) || x.Blog.BlogTitle.Contains(key)).ToList();
            }
        }
    }
}
