using System.Collections.Generic;

namespace Infrastructure.Data.Abstractions
{
    public interface ICrudRepository<T,ID>
        :IRepository
    {
        long Count();
        bool Exists(ID id);
        T Save(T entity);
        IEnumerable<T> Save(IEnumerable<T> entities);
        T FindOne(ID id);
        IEnumerable<T> FindAll();
        void Delete(ID id);
        void Delete(T entity);
        void Delete(IEnumerable<T> entities);
        void DeleteAll();
    }
}
