using System.Collections.Generic;

namespace Infrastructure.Data.Abstractions
{
    public interface IPagingAndSortingRepository<T, ID>
        : ICrudRepository<T, ID>
    {
        IEnumerable<T> FindAll(Sort sort);
        Page<T> FindAll(Pageable pageable);
    }
}
