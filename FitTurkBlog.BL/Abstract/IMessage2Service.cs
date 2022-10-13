﻿using FitTurkBlog.Entities.Concrete;
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
        List<Message2> GetSendBoxListByWriter(int id);
        List<Message2> GetTrashBoxListByWriter();
        List<Message2> GetImportantBoxListByWriter();

    }
}
