using FitTurkBlog.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTurkBlog.DAL.Abstract
{
    public interface IMessage2DAL : IGenericDAL<Message2>
    {
        List<Message2> GetListInBoxWithMessageByWriter(int id);
        List<Message2> GetListInBoxWithMessageByKey(int id,string key);
        List<Message2> GetListSendBoxWithMessageByWriter(int id);
        List<Message2> GetListSendBoxWithMessageByKey(int id,string key);
        List<Message2> GetListByKey(string key);
        List<Message2> GetListAllMessage();
        List<Message2> GetListTrashBoxWithMessageByWriter();
        List<Message2> GetListTrashBoxWithMessageByKey(string key);
        List<Message2> GetListImportantBoxWithMessageByWriter();
        List<Message2> GetListImportantBoxWithMessageByKey(string key);
        Message2 GetMessageByIdWithSenderAndReceiver(int id);

    }
}
