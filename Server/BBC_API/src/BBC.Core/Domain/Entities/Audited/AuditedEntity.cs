using System;
using System.Collections.Generic;
using System.Text;

namespace BBC.Core.Domain.Entities.Audited
{
    public abstract class AuditedEntity<TId> : EntityBase<TId>, IAuditedEntity<TId>
        where TId : IEquatable<TId>
    {
        public bool isDeleted { get; set; } = false;
        public bool isActive { get; set; } = true;
    }
}
