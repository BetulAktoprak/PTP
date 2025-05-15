using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using PTP.EntityLayer.Abstractions;

namespace PTP.Business.Abstractions
{
    public interface IService<T> where T : BaseEntity
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(int id);
        T? GetByID(int id);
        IEnumerable<T>? GetAll();
        IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null);
    }
}
