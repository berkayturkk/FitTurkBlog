using FitTurkBlog.BL.Abstract;
using FitTurkBlog.DAL.Abstract;
using FitTurkBlog.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTurkBlog.BL.Concrete
{
    public class NotificationManager : INotificationService
    {
        INotificationDAL _notificationDAL;

        public NotificationManager(INotificationDAL notificationDAL)
        {
            _notificationDAL = notificationDAL;
        }

        public void Add(Notification t)
        {
            _notificationDAL.Add(t);
        }

        public void Delete(Notification t)
        {
            _notificationDAL.Delete(t);
        }

        public List<Notification> GetList()
        {
            return _notificationDAL.ListAll();
        }

        public List<Notification> GetListNotificationByKey(string key)
        {
            return _notificationDAL.GetListNotificationByKey(key);
        }

        public Notification TGetById(int id)
        {
            return _notificationDAL.GetById(id);
        }

        public void Update(Notification t)
        {
            _notificationDAL.Update(t);
        }
    }
}
