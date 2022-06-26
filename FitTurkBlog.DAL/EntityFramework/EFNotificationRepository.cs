﻿using FitTurkBlog.DAL.Abstract;
using FitTurkBlog.DAL.Repositories;
using FitTurkBlog.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTurkBlog.DAL.EntityFramework
{
    public class EFNotificationRepository : GenericRepository<Notification>,INotificationDAL
    {
    }
}
