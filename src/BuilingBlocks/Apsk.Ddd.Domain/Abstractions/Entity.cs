// <copyright file="Entity.cs" company="apsk">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System.Collections.Generic;

namespace Apsk.Ddd.Domain.Abstractions
{
    public abstract class Entity
        : IEntity
    {
        /// <inheritdoc/>
        public abstract object[] GetKeys();

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"Entity:{GetType()},Keys:{string.Join(",", GetKeys())}";
        }
    }

    public abstract class Entity<TKey>
        : IEntity<TKey>
    {
        int? _requestHashcode;

        public TKey Id { get; set; }

        /// <inheritdoc/>
        public object[] GetKeys()
        {
            return new object[] { Id };
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Entity<TKey>))
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            if (GetType() != obj.GetType())
                return false;

            var item = obj as Entity<TKey>;

            if (IsTransient() || item.IsTransient())
                return false;

            return item.Id.Equals(Id);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"Entity:{GetType()},Id:{Id}";
        }

        public static bool operator ==(Entity<TKey> left, Entity<TKey> right)
        {
            return left == null || right == null ? false : left.Equals(right);
        }

        public static bool operator !=(Entity<TKey> left, Entity<TKey> right)
        {
            return !(left == right);
        }

        private bool IsTransient()
        {
            return EqualityComparer<TKey>.Default.Equals(Id,default);
        }
    }
}
