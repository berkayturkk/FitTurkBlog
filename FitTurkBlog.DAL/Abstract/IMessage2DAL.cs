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
        List<Message2> GetListSendBoxWithMessageByWriter(int id);
        List<Message2> GetListTrashBoxWithMessageByWriter();
        List<Message2> GetListImportantBoxWithMessageByWriter();

    }
}
