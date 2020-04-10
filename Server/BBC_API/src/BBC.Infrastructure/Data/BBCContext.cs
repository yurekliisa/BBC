using BBC.Core.Domain;
using BBC.Core.Domain.Identity;
using Microsoft.AspNetCore.Identity;
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
        public DbSet<JobAdvert> JobAdverts { get; set; }
        //public DbSet<JobUser> JobUsers { get; set; }
        public DbSet<Media> Medias { get; set; }
        public DbSet<LobiMessages> LobiMessages { get; set; }
        public DbSet<LobiUser> LobiUsers { get; set; }
        public DbSet<Popularity> Popularities { get; set; }
        public DbSet<PrivateMessages> PrivateMessages { get; set; }
        public DbSet<SocialMedia> SocialMedias { get; set; }
        public DbSet<TarifAndRecete> TarifAndRecetes { get; set; }
        //public DbSet<Template> Templates { get; set; }
        public DbSet<JobsApplication> JobsApplications { get; set; }
        public DbSet<TaRCategory> TaRCategories { get; set; }
        public DbSet<Settings> Settings { get; set; }

        public BBCContext(DbContextOptions<BBCContext> options):base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region Tarif And Recete - Category (M-M)
            modelBuilder.Entity<TaRCategory>().HasKey(tarc => new { tarc.CategoryId, tarc.TarifAndReceteId });
            modelBuilder.Entity<TaRCategory>()
                .HasOne(tarc => tarc.TarifAndRecete)
                .WithMany(tar => tar.TaRCategories)
                .HasForeignKey(fk => fk.TarifAndReceteId);
            modelBuilder.Entity<TaRCategory>()
                .HasOne(tarc => tarc.Category)
                .WithMany(tar => tar.TaRCategories)
                .HasForeignKey(fk => fk.CategoryId);
            #endregion

            #region Lobi - User (M-M)
            modelBuilder.Entity<LobiUser>().HasKey(tarc => new { tarc.LobiId, tarc.UserId });
            modelBuilder.Entity<LobiUser>()
                .HasOne(lu=> lu.User)
                .WithMany(u => u.LobiUsers)
                .HasForeignKey(lu => lu.UserId);
            modelBuilder.Entity<LobiUser>()
               .HasOne(lu => lu.Lobi)
               .WithMany(u => u.LobiUsers)
               .HasForeignKey(lu => lu.LobiId);
            #endregion
        }

    }
}
