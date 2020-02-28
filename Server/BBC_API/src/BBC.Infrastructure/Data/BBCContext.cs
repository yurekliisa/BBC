using BBC.Core.Domain;
using BBC.Core.Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BBC.Infrastructure.Data
{
    public class BBCContext: IdentityDbContext<User,Role,int>, IBBCContext
    {
        public DbSet<Category> Categories { get; set; }
        public BBCContext(DbContextOptions<BBCContext> options):base(options)
        {

        }
    }
}
