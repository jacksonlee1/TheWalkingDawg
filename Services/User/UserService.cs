using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models.User;



namespace Services.User
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _db;
        private readonly int? _userId;

        public UserService(ApplicationDbContext db)
        {
            _db = db;

        }


        public async Task<bool> RegisterUserAsync(UserRegister model)
        {
            if (GetUserByUserNameAsync(model.Username) is null) return false;


            var entity = new UserEntity
            {
                Username = model.Username,
                Address = model.Address,
                PhoneNum = model.PhoneNum,
                Name = model.Name


            };
            var passwordHasher = new PasswordHasher<UserEntity>();
            entity.Password = passwordHasher.HashPassword(entity, model.Password);
            _db.Users.Add(entity);
            var numChanges = await _db.SaveChangesAsync();
            return numChanges == 1;

        }

        public async Task<UserEntity?> GetUserByUserNameAsync(string username)
        {
            return await _db.Users.FirstOrDefaultAsync(u => u.Username.ToLower() == username.ToLower());
        }

        public async Task<UserEntity?> GetUserByIdAsync(int id)
        {
            return await _db.Users.Include(u => u.Dogs).FirstOrDefaultAsync(x => x.Id == id);

        }

        //Admin service? No Auth in user service
        public async Task<bool> UpdateUserAsync(UserUpdate req)
        {

            var entity = await _db.Users.FindAsync(req.Id);
            if(entity is null) return false;
            if (entity.Id != _userId) return false;
            entity.Username = req.Username;
            entity.Name = req.Name;
            entity.Address = req.Address;
            entity.PhoneNum = req.PhoneNum;

            var passwordHasher = new PasswordHasher<UserEntity>();
            entity.Password = passwordHasher.HashPassword(entity, req.Password);
            var numChanges = await _db.SaveChangesAsync();
            return numChanges == 1;
        }
        public async Task<bool> DeleteUserByIdAsync(int id)
        {
            var entity = await _db.Users.FindAsync(id);
            if(entity is null )return false;
            _db.Users.Remove(entity);
            var numChanges = await _db.SaveChangesAsync();
            return numChanges == 1;

        }

        public async Task<IOrderedEnumerable<UserDetail>> SortWalkersByAverageRating(bool descending)
        {
            var users = await _db.Users.Include(u => u.Reviews).Select(u => new UserDetail
                {
                    Username = u.Username,
                    AverageRating = u.AverageRating,
                    Name = u.Name,
                    Address = u.Address,
                    PhoneNum = u.PhoneNum
                }).ToListAsync();

                 return !descending?users.OrderBy(u=> u.AverageRating):users.OrderByDescending(u=> u.AverageRating);

            
        }


        public async Task<List<UserDetail>> GetAllUsersAsync()
        {
            return await _db.Users.Include(u => u.Reviews).Select(u => new UserDetail
            {
                Username = u.Username,
                AverageRating = u.AverageRating,
                Name = u.Name,
                Address = u.Address,
                PhoneNum = u.PhoneNum
            }).ToListAsync();


        }

    }
}