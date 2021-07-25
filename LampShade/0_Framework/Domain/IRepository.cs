using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace _0_Framework.Domain
{
    public interface IRepository<in TKey , T> where T : class
    {
        void Create(T entity);
        T Get(TKey id);
        List<T> Get();
        void SaveChanges();
        bool Exists(Expression<Func<T, bool>> expression);
    }
}
