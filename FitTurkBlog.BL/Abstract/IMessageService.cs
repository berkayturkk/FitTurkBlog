using FitTurkBlog.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTurkBlog.BL.Abstract
{
    public interface IMessageService : IGenericService<Message>
    {
        List<Message> GetInBoxListByWriter(string mail);
    }
}
