using BBC.Core.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BBC.Infrastructure.Data
{
    public class BBCContext:DbContext, IBBCContext
    {
        public DbSet<Category> Categories { get; set; }
        public BBCContext(DbContextOptions<BBCContext> options):base(options)
        {

        }
    }
}
