using Microsoft.EntityFrameworkCore;
using OnlineStore.Data;
using OnlineStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Services
{
    public class UserModelService : IUserModelService
    {
        private readonly ApplicationDbContext context;

        public UserModelService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<UserModel>> GetAllItemsAsync()
        {
            var users = await context.Users
                .ToListAsync();
            return users;
        }
    }
}
