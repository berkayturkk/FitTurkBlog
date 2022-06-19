using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FitTurkBlog.DAL.Abstract
{
    public interface IGenericDAL<T> where T : class
    {
        List<T> ListAll();
        void Add(T item);
        void Delete(T item);
        void Update(T item);
        T GetById(int id);
        List<T> GetListAll(Expression<Func<T, bool>> filter);

    }
}
