using BBC.Core.Domain;
using BBC.Services.Services.SocialMediaService.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BBC.Services.Identity.Dto.UserDtos
{
    public class EditUserDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public virtual string Name { get; set; }
        public virtual string SurName { get; set; }
        public virtual string Photo { get; set; }
        public virtual string About { get; set; }
        public virtual DateTime Birthday { get; set; }
        public SocialMediaDto SocialMedia { get; set; }

        public EditUserDto()
        {
            SocialMedia = new SocialMediaDto();
        }
    }
}
