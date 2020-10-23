// <copyright file="ITransaction.cs" company="apsk">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Apsk.Ddd.Domain.Abstractions
{
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore.Storage;

    public interface ITransaction
    {
        IDbContextTransaction GetCurrentTransaction();

        bool HasActiveTransaction();

        Task<IDbContextTransaction> BeginTransactionAsync();

        Task CommitTransactionAsync(IDbContextTransaction transaction);
    }
}
