// <copyright file="EfCoreContext.cs" company="apsk">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Apsk.Ddd.Repositories
{
    using System.Threading;
    using System.Threading.Tasks;
    using Apsk.Ddd.Domain.Abstractions;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Storage;

    public class EfCoreContext
        : DbContext, IUnitOfWork, ITransaction
    {
        public EfCoreContext(DbContextOptions options)
            : base(options)
        { }

        public Task<IDbContextTransaction> BeginTransactionAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task CommitTransactionAsync(IDbContextTransaction transaction)
        {
            throw new System.NotImplementedException();
        }

        public IDbContextTransaction GetCurrentTransaction()
        {
            throw new System.NotImplementedException();
        }

        public bool HasActiveTransaction()
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            await SaveChangesAsync();
            return true;
        }
    }
}
