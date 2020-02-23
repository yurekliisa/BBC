using System;
using System.Collections.Generic;
using System.Text;

namespace BBC.Core.Domain.Entities
{
    public interface IEntityBase<TId> where TId : IEquatable<TId>
    {
        TId Id { get; set; }
    }
}
