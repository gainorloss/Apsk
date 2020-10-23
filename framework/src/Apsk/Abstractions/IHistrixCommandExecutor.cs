// <copyright file="IHistrixCommandExecutor.cs" company="apsk">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Apsk.Abstractions
{
    using System;
    using System.Threading.Tasks;

    public interface IHistrixCommandExecutor
    {
        void Execute(Action action);

        Task ExecuteAsync(Func<Task> action);
    }
}
