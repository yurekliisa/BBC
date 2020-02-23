using System;
using System.Collections.Generic;
using System.Text;

namespace BBC.Core.Domain.Entities
{
    public abstract class EntityBase<TId> : IEntityBase<TId> where TId : IEquatable<TId>
    {
        public virtual TId Id { get; set; }
    }
}
