using System;
using System.Collections.Generic;
using System.Text;

namespace BBC.Services.Services.Common.Dto
{
    /// <summary>
    /// DTO MarkUp
    /// </summary>
    public class BaseDto<TKey> where TKey : IEquatable<TKey>
    {
        public TKey Id { get; set; }
    }

    public class BaseDto
    {

    }

}
