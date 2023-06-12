using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Entities;
using Microsoft.EntityFrameworkCore;


namespace Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DogsEntity>()
            .HasOne(d => d.Owner)
            .WithMany(d => d.Dogs)
            .HasForeignKey(d => d.OwnerId);

            modelBuilder.Entity<RatingEntity>()
         .HasOne(r => r.Author)
         .WithMany(d => d.Ratings)
         .HasForeignKey(d => d.AuthorId);

        //  modelBuilder.Entity<RatingEntity>()
        //  .HasOne(r=> r.Walk)
        //  .WithMany(r=>r.Ratings)
        //  .HasForeignKey(r => r.WalkId);

        }

        public DbSet<UserEntity> Users { get; set; }

        public DbSet<DogsEntity> Dogs { get; set; }

        public DbSet<RatingEntity> Ratings { get; set; }

    }


}