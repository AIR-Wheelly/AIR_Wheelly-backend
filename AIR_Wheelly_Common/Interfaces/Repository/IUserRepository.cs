﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIR_Wheelly_Common.Models;

namespace AIR_Wheelly_Common.Interfaces.Repository
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetUserByEmailAsync(string email);
        Task<User?> GetUserByIdAsync(string id);
        Task UpdateUserAsync(User user);

    }
}
