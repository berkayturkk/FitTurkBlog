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
        List<Message2> GetListWithMessageByWriter(int id);
    }
}
