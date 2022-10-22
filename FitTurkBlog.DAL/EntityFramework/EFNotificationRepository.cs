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
    public class EFNotificationRepository : GenericRepository<Notification>,INotificationDAL
    {
        public List<Notification> GetListNotificationByKey(string key)
        {
            using (var sqlDbContext = new SqlDbContext())
            {
                return sqlDbContext.Notifications.Where(x => x.NotificationDetails.Contains(key)).ToList();
            }
        }
    }
}
