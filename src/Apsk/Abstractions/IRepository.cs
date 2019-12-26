// <copyright file="IRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Apsk.Abstractions
{
    public interface IRepository<T, ID>
        where T : Entity<ID>
    {
        /// <summary>
        /// generate id
        /// </summary>
        /// <returns></returns>
        ID GenerateId();

        /// <summary>
        /// load
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T LoadByIdAsync(ID id);
    }
}
