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
        private readonly int? _userId;

        public UserService(ApplicationDbContext db)
        {
            _db = db;
            
        }


        public async Task<bool> RegisterUserAsync(UserRegister model)
        {

            var entity = new UserEntity{
                Username =model.Username,
                Address = model.Address,
                PhoneNum = model.PhoneNum,
                Name = model.Name

                
            };
            var passwordHasher = new PasswordHasher<UserEntity>();
            entity.Password = passwordHasher.HashPassword(entity,model.Password);
            _db.Users.Add(entity);
            var numChanges = await _db.SaveChangesAsync();
            return numChanges == 1;

        }

        public async Task<UserEntity> GetUserByUserNameAsync(string username)
        {
            return await _db.Users.FirstOrDefaultAsync(u => u.Username.ToLower() == username.ToLower());
        }

        public async Task<UserEntity> GetUserByIdAsync(int id)
        {
            return await _db.Users.FindAsync(id);
           
        }
        
        //Admin service? No Auth in user service
        public async Task<bool> UpdateUserAsync(UserUpdate req){
           
            var entity = await _db.Users.FindAsync(req.Id);
            // if (entity.UserId != _userId) return false;
            entity.Username = req.Username;
            entity.Name = req.Name;
            entity.Address = req.Address;
            entity.PhoneNum = req.PhoneNum;

             var passwordHasher = new PasswordHasher<UserEntity>();
            entity.Password = passwordHasher.HashPassword(entity,req.Password);
            var numChanges = await _db.SaveChangesAsync();
            return numChanges == 1;
        }
         public async Task<bool> DeleteUserByIdAsync(int id)
        {
            var note = await _db.Users.FindAsync(id);

            _db.Users.Remove(note);
            var numChanges = await _db.SaveChangesAsync();
            return numChanges == 1;

        }
    }
}