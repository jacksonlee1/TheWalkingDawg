using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Entities;
using Models.User;

namespace Services.User
{
    public interface IUserService
    {
        Task<bool> RegisterUserAsync(UserRegister model);

        Task<UserEntity> GetUserByIdAsync(int id);
        Task<List<UserDetail>> SortWalkersByAverageRating(bool decending);
        Task<List<UserDetail>> GetAllUsersAsync();
    }
}