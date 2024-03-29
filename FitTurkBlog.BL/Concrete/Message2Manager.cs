﻿using FitTurkBlog.BL.Abstract;
using FitTurkBlog.DAL.Abstract;
using FitTurkBlog.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTurkBlog.BL.Concrete
{
    public class Message2Manager : IMessage2Service
    {
        IMessage2DAL _message2DAL;

        public Message2Manager(IMessage2DAL message2DAL)
        {
            _message2DAL = message2DAL;
        }

        public void Add(Message2 t)
        {
            _message2DAL.Add(t);
        }

        public void Delete(Message2 t)
        {
            _message2DAL.Delete(t);
        }

        public List<Message2> GetInBoxListByWriter(int id)
        {
            return _message2DAL.GetListInBoxWithMessageByWriter(id);
        }

        public List<Message2> GetSendBoxListByWriter(int id)
        {
            return _message2DAL.GetListSendBoxWithMessageByWriter(id);
        }

        public List<Message2> GetList()
        {
            return _message2DAL.ListAll();
        }

        public Message2 TGetById(int id)
        {
            return _message2DAL.GetById(id);
        }

        public void Update(Message2 t)
        {
            _message2DAL.Update(t);

        }

        public List<Message2> GetTrashBoxListByWriter(int id)
        {
            return _message2DAL.GetListTrashBoxWithMessageByWriter(id);
        }

        public List<Message2> GetImportantBoxListByWriter(int id)
        {
            return _message2DAL.GetListImportantBoxWithMessageByWriter(id);
        }

        public List<Message2> GetListAllByKey(string key,int id)
        {
            return _message2DAL.GetListByKey(key,id);
        }

        public List<Message2> GetListAllMessage(int id)
        {
            return _message2DAL.GetListAllMessage(id);
        }

        public List<Message2> GetInBoxListByKey(int id, string key)
        {
            return _message2DAL.GetListInBoxWithMessageByKey(id, key);  
        }

        public List<Message2> GetSendBoxListByKey(int id, string key)
        {
            return _message2DAL.GetListSendBoxWithMessageByKey(id, key);
        }
        public List<Message2> GetTrashBoxListByKey(string key, int id)
        {
            return _message2DAL.GetListTrashBoxWithMessageByKey(key,id);
        }

        public List<Message2> GetImportantBoxListByKey(string key, int id)
        {
            return _message2DAL.GetListImportantBoxWithMessageByKey(key,id);
        }

        public Message2 GetMessageByIdWithSenderAndReceiver(int id)
        {
            return _message2DAL.GetMessageByIdWithSenderAndReceiver(id);
        }
    }
}
