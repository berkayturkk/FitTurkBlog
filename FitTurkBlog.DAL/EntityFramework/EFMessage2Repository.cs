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

        public List<Message2> GetListInBoxWithMessageByWriter(int id)
        {
            using (var sqlDbContext = new SqlDbContext())
            {
                return sqlDbContext.Messages2.Include(x => x.MessageSenderUser).Include(x => x.MessageReceiverUser).Where(x => x.MessageReceiverID == id && x.IsDeleted == false && x.IsImportant == false).ToList();
            }
        }

        public List<Message2> GetListSendBoxWithMessageByWriter(int id)
        {
            using (var sqlDbContext = new SqlDbContext())
            {
                return sqlDbContext.Messages2.Include(x => x.MessageSenderUser).Include(x => x.MessageReceiverUser).Where(x => x.MessageSenderID == id && x.IsDeleted == false && x.IsImportant == false).ToList();
            }
        }

        public List<Message2> GetListTrashBoxWithMessageByWriter()
        {
            using (var sqlDbContext = new SqlDbContext())
            {
                return sqlDbContext.Messages2.Include(x => x.MessageSenderUser).Include(y => y.MessageReceiverUser).Where(z => z.IsDeleted == true && z.IsImportant == false).ToList();
            }
        }
        public List<Message2> GetListImportantBoxWithMessageByWriter()
        {
            using (var sqlDbContext = new SqlDbContext())
            {
                return sqlDbContext.Messages2.Include(x => x.MessageSenderUser).Include(y => y.MessageReceiverUser).Where(z => z.IsImportant == true).ToList();
            }
        }

        
    }
}
