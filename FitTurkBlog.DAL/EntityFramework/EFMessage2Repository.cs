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

        public List<Message2> GetListTrashBoxWithMessageByKey(string key)
        {
            using (var sqlDbContext = new SqlDbContext())
            {
                return sqlDbContext.Messages2.Include(x => x.MessageSenderUser).Include(x => x.MessageReceiverUser).Where(x => (x.MessageSenderUser.WriterMail.Contains(key) || x.MessageReceiverUser.WriterMail.Contains(key) || x.MessageSubject.Contains(key) || x.MessageDetails.Contains(key)) && x.IsDeleted == true && x.IsImportant == false).ToList();
            }
        }
        public List<Message2> GetListTrashBoxWithMessageByWriter()
        {
            using (var sqlDbContext = new SqlDbContext())
            {
                return sqlDbContext.Messages2.Include(x => x.MessageSenderUser).Include(x => x.MessageReceiverUser).Where(x =>x.IsDeleted == true && x.IsImportant == false).ToList();
            }
        }

        public List<Message2> GetListImportantBoxWithMessageByKey(string key)
        {
            using (var sqlDbContext = new SqlDbContext())
            {
                return sqlDbContext.Messages2.Include(x => x.MessageSenderUser).Include(x => x.MessageReceiverUser).Where(x => (x.MessageSenderUser.WriterMail.Contains(key) || x.MessageReceiverUser.WriterMail.Contains(key) || x.MessageSubject.Contains(key) || x.MessageDetails.Contains(key)) && x.IsImportant == true).ToList();
            }
        }
        public List<Message2> GetListImportantBoxWithMessageByWriter()
        {
            using (var sqlDbContext = new SqlDbContext())
            {
                return sqlDbContext.Messages2.Include(x => x.MessageSenderUser).Include(x => x.MessageReceiverUser).Where(x =>x.IsImportant == true).ToList();
            }
        }

        public List<Message2> GetListByKey(string key)
        {
            using (var sqlDbContext = new SqlDbContext())
            {
                return sqlDbContext.Messages2.Include(x => x.MessageSenderUser).Include(x => x.MessageReceiverUser).Where(x => x.MessageSenderUser.WriterMail.Contains(key) || x.MessageReceiverUser.WriterMail.Contains(key) || x.MessageSubject.Contains(key) || x.MessageDetails.Contains(key)).ToList();
            }
        }

        public List<Message2> GetListAllMessage()
        {
            using (var sqlDbContext = new SqlDbContext())
            {
                return sqlDbContext.Messages2.Include(x => x.MessageSenderUser).Include(y => y.MessageReceiverUser).ToList();
            }
        }

        public List<Message2> GetListInBoxWithMessageByKey(int id, string key)
        {
            using (var sqlDbContext = new SqlDbContext())
            {
                return sqlDbContext.Messages2.Include(x => x.MessageSenderUser).Include(x => x.MessageReceiverUser).Where(x => (x.MessageSenderUser.WriterMail.Contains(key)  || x.MessageReceiverUser.WriterMail.Contains(key) || x.MessageSubject.Contains(key) || x.MessageDetails.Contains(key))&& x.MessageReceiverID == id && x.IsDeleted == false && x.IsImportant == false).ToList();
            }
        }

        public List<Message2> GetListSendBoxWithMessageByKey(int id, string key)
        {
            using (var sqlDbContext = new SqlDbContext())
            {
                return sqlDbContext.Messages2.Include(x => x.MessageSenderUser).Include(x => x.MessageReceiverUser).Where(x => (x.MessageSenderUser.WriterMail.Contains(key) || x.MessageReceiverUser.WriterMail.Contains(key) || x.MessageSubject.Contains(key) || x.MessageDetails.Contains(key)) && x.MessageSenderID == id && x.IsDeleted == false && x.IsImportant == false).ToList();
            }
        }
    }
}
