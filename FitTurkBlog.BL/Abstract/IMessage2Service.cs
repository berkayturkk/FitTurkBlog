using FitTurkBlog.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTurkBlog.BL.Abstract
{
    public interface IMessage2Service : IGenericService<Message2>
    {
        List<Message2> GetInBoxListByWriter(int id);
        List<Message2> GetInBoxListByKey(int id,string key);
        List<Message2> GetSendBoxListByWriter(int id);
        List<Message2> GetSendBoxListByKey(int id,string key);
        List<Message2> GetListAllByKey(string key,int id);
        List<Message2> GetListAllMessage(int id);
        List<Message2> GetTrashBoxListByWriter(int id);
        List<Message2> GetTrashBoxListByKey(string key, int id);
        List<Message2> GetImportantBoxListByWriter(int id);
        List<Message2> GetImportantBoxListByKey(string key, int id);
        Message2 GetMessageByIdWithSenderAndReceiver(int id);

    }
}
