using System.Threading.Tasks;

namespace Apsk.Abstractions
{
    public abstract class RepositoryBase<T, ID> : IRepository<T, ID>
        where T : Entity<ID>
    {
        /// <inheritdoc/>
        public ID GenerateId()
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc/>
        public T LoadByIdAsync(ID id)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> SaveAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}
