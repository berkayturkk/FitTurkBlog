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
        List<Message2> GetListAllByKey(string key);
        List<Message2> GetListAllMessage();
        List<Message2> GetTrashBoxListByWriter();
        List<Message2> GetTrashBoxListByKey(string key);
        List<Message2> GetImportantBoxListByWriter();
        List<Message2> GetImportantBoxListByKey(string key);
        Message2 GetMessageByIdWithSenderAndReceiver(int id);

    }
}
