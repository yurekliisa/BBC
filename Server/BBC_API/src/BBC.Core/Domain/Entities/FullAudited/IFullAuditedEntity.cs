using BBC.Core.Domain.Entities.Audited;
using System;
using System.Collections.Generic;
using System.Text;

namespace BBC.Core.Domain.Entities.FullAudited
{
    public interface IFullAuditedEntity<TId> : IAuditedEntity<TId> where TId : IEquatable<TId>
    {
        DateTime CreateTime { get; set; }
        TId CreateUserId { get; set; }

        DateTime UpdateTime { get; set; }
        TId UpdateUserId { get; set; }

        DateTime DeleteTime { get; set; }
        TId DeleteId { get; set; }

    }
}
