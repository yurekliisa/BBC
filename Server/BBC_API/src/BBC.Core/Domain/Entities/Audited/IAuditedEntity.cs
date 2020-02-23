using System;
using System.Collections.Generic;
using System.Text;

namespace BBC.Core.Domain.Entities.Audited
{
    public interface IAuditedEntity<TId> : IEntityBase<TId>
          where TId : IEquatable<TId>
    {
        bool isDeleted { get; set; }
        bool isActive { get; set; }
    }
}
