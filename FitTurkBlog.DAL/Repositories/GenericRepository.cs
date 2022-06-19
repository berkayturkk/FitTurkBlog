using FitTurkBlog.DAL.Abstract;
using FitTurkBlog.DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FitTurkBlog.DAL.Repositories
{
    public class GenericRepository<T> : IGenericDAL<T> where T : class
    {
        SqlDbContext sqlDbContext = new SqlDbContext();
        public void Add(T t)
        {
            sqlDbContext.Add(t);
            sqlDbContext.SaveChanges();
        }

        public void Delete(T t)
        {
            sqlDbContext.Remove(t);
            sqlDbContext.SaveChanges();
        }

        public T GetById(int id)
        {
            return sqlDbContext.Set<T>().Find(id);
        }

        public List<T> GetListAll(Expression<Func<T, bool>> filter)
        {
            return sqlDbContext.Set<T>().Where(filter).ToList();
        }

        public List<T> ListAll()
        {
            return sqlDbContext.Set<T>().ToList();
        }

        public void Update(T t)
        {
            sqlDbContext.Update(t);
            sqlDbContext.SaveChanges();
        }
    }
}
