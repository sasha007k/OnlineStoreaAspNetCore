using OnlineStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Services
{
    public interface IUserModelService
    {
        Task<IEnumerable<UserModel>> GetAllItemsAsync();
    }
}
