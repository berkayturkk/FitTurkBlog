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
        public List<Comment> GetListComment()
        {
            using (var sqlDbContext = new SqlDbContext())
            {
                return sqlDbContext.Comments.Include(x => x.Blog).ToList();
            }
        }
    }
}
