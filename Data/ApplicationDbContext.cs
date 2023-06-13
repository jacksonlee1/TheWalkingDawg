using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Entities;
using Microsoft.EntityFrameworkCore;


namespace Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {

        }
        public DbSet<WalkingEntity> Walking{get;set;}
        public DbSet<UserEntity> Users{get;set;}
        
    }


}