using System.Collections.Generic;

namespace S.U.TEST.Repositories
{
    public interface IRepositoryBase<T> where T : class
    {
        void Delete(T entity);
        void Insert(T entity);
        IEnumerable<T> Query(string where = null);
        T SingleQuery(int id);
        void Update(T entity);
    }
}