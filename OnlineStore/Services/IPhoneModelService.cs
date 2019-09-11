using OnlineStore.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineStore.Services
{
    public interface IPhoneModelService
    {
        Task<IEnumerable<PhoneModel>> GetAllItemsAsync();

        Task<bool> AddPhoneAsync(PhoneModel newPhone);

        Task<bool> CreateDiscountAsync(PhoneModel newPhone);

        Task<bool> DeletePhoneAsync(Guid id);
    }
}
