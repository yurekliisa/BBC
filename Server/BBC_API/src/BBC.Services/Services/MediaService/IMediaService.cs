using BBC.Services.Services.Common.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BBC.Services.Services.MediaService
{
    class IMediaService : IBaseService
    {
        Task<List<MediaListDto>> GetAllMedia();

    }
}
