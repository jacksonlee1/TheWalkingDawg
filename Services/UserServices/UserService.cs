using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models.User;


namespace Services.UserServices
{
    public class UserService:IUserService
    {
        private readonly ApplicationDbContext _db;

        public UserService(ApplicationDbContext db)
        {
            _db = db;
        }


        public async Task<bool> RegisterUserAsync(UserRegister model)
        {

            var entity = new UserEntity{
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName
            };
            var passwordHasher = new PasswordHasher<UserEntity>();
            entity.Password = passwordHasher.HashPassword(entity,model.Password);
            _db.Users.Add(entity);
            var numChanges = await _db.SaveChangesAsync();
            return numChanges == 1;

        }

        public async Task<UserEntity> GetUserByEmailAsync(string email)
        {
            return await _db.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
        }

        public async Task<UserEntity> GetUserByIdAsync(int id)
        {
            return await _db.Users.FindAsync(id);
           
        }
    }
}