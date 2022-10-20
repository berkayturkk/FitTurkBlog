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
        List<Message2> GetListByKey(string key,int id);
        List<Message2> GetListAllMessage(int id);
        List<Message2> GetListTrashBoxWithMessageByWriter(int id);
        List<Message2> GetListTrashBoxWithMessageByKey(string key, int id);
        List<Message2> GetListImportantBoxWithMessageByWriter(int id);
        List<Message2> GetListImportantBoxWithMessageByKey(string key, int id);
        Message2 GetMessageByIdWithSenderAndReceiver(int id);

    }
}
