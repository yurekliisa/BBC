using BBC.Core.Domain.Entities.Audited;
using System;
using System.Collections.Generic;
using System.Text;

namespace BBC.Core.Domain.Entities.FullAudited
{
    public abstract class FullAuditedEntity<TId> : AuditedEntity<TId>, IFullAuditedEntity<TId>
         where TId : IEquatable<TId>
    {
        public DateTime CreateTime { get; set; } = DateTime.Now;
        public TId CreateUserId { get; set; }
        public DateTime UpdateTime { get; set; }
        public TId UpdateUserId { get; set; }
        public DateTime DeleteTime { get; set; }
        public TId DeleteId { get; set; }
    }
}
