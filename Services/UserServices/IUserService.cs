using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Entities;
using Models.User;

namespace Services.UserServices
{
    public interface IUserService
    {
        Task<bool> RegisterUserAsync(UserRegister model);

        Task<UserEntity> GetUserByIdAsync(int id);
    }
}