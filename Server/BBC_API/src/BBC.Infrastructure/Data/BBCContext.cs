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
        public DbSet<Country> Countries { get; set; }
        public DbSet<Content> Contents { get; set; }
        public DbSet<Lobi> Lobis { get; set; }
        public DbSet<JobAdvert> Jobs { get; set; }
        public DbSet<JobUser> JobUsers { get; set; }
        public DbSet<Media> Medias { get; set; }
        public DbSet<LobiMessages> LobiMessages { get; set; }
        public DbSet<LobiUser> LobiUsers { get; set; }
        public DbSet<Popularity> Popularities { get; set; }
        public DbSet<PrivateMessages> PrivateMessages { get; set; }
        public DbSet<SocialMedia> SocialMedias { get; set; }
        public DbSet<TarifAndRecete> TarifAndRecetes { get; set; }
        public DbSet<Template> Templates { get; set; }
        public DbSet<JobsApplication> JobsApplications { get; set; }
        public DbSet<TaRCategory> ToRCategories { get; set; }

        public BBCContext(DbContextOptions<BBCContext> options):base(options)
        {

        }
    }
}
