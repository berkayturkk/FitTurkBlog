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
    public class EFMessage2Repository : GenericRepository<Message2>, IMessage2DAL
    {
        public List<Message2> GetListWithMessageByWriter(int id)
        {
            using (var sqlDbContext = new SqlDbContext())
            {
                return sqlDbContext.Messages2.Include(x => x.MessageSenderUser).Where(x => x.MessageReceiverID == id).ToList();
            }
        }
    }
}
