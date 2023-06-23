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
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //FK Rating to Users(OwnerId)
            modelBuilder.Entity<RatingEntity>()
            .HasOne(r => r.Owner)
            .WithMany(d => d.UserReviews)
            .HasForeignKey(d => d.OwnerId);
            //FK Rating To Users(WalkerId)
            modelBuilder.Entity<RatingEntity>()
            .HasOne<UserEntity>(r => r.Walker)
            .WithMany(u => u.Reviews)
            .HasForeignKey(r => r.WalkerId);
            //FK Dogs To Users(OwnerId)
            modelBuilder.Entity<UserEntity>()
            .HasMany<DogsEntity>(user => user.Dogs)
            .WithOne(dog => dog.Owner)
            .HasForeignKey(dog => dog.OwnerId);
            //FK Walks to Users(WalkerId)
            modelBuilder.Entity<UserEntity>()
            .HasMany<WalkingEntity>(u => u.Walks)
            .WithOne(w => w.Walker)
            .HasForeignKey(w => w.WalkerId);
            //FK Walks to Dog(DogId)
            modelBuilder.Entity<WalkingEntity>()
            .HasOne(w => w.Dog)
            .WithMany(d => d.walks)
            .HasForeignKey(w => w.DogId);
        }
        public DbSet<UserEntity> Users { get; set; } = null!;
        public DbSet<DogsEntity> Dogs { get; set; } = null!;
        public DbSet<WalkingEntity> Walks { get; set; } = null!;
        public DbSet<RatingEntity> Ratings { get; set; } = null!;
    }
}