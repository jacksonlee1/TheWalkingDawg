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
        //     modelBuilder.Entity<DogsEntity>()
        //     .HasOne(d => d.Owner)
        //     .WithMany(d => d.Dogs)
        //     .HasForeignKey(d => d.OwnerId);

        //     modelBuilder.Entity<RatingEntity>()
        //  .HasOne(r => r.Owner)
        //  .WithMany(d => d.Ratings)
        //  .HasForeignKey(d => d.OwnerId);

    


         modelBuilder.Entity<UserEntity>()
         .HasMany<RatingEntity>(user=>user.UserReviews)
         .WithOne(rating=>rating.Owner)
         .HasForeignKey(rating => rating.OwnerId);

            modelBuilder.Entity<UserEntity>()
         .HasMany<RatingEntity>(user=>user.Reviews)
         .WithOne(rating=>rating.Walker)
         .HasForeignKey(rating => rating.WalkerId);

        modelBuilder.Entity<UserEntity>()
        .HasMany<DogsEntity>(user => user.Dogs)
        .WithOne(dog => dog.Owner)
        .HasForeignKey(dog=>dog.OwnerId);

        //  modelBuilder.Entity<RatingEntity>()
        //  .HasOne(r=> r.Walk)
        //  .WithMany(r=>r.Ratings)
        //  .HasForeignKey(r => r.WalkId);


        modelBuilder.Entity<WalkingEntity>()
        .HasOne(w => w.Dog).WithMany(d => d.walks).HasForeignKey(w => w.DogId);

        }

        public DbSet<UserEntity> Users { get; set; }

        public DbSet<DogsEntity> Dogs { get; set; }
         public DbSet<WalkingEntity> Walking{get;set;}

        public DbSet<RatingEntity> Ratings { get; set; }


    }


}