using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Data;
using OnlineStore.Models;

namespace OnlineStore.Services
{
    public class PhoneModelService : IPhoneModelService
    {
        private readonly ApplicationDbContext context;

        public PhoneModelService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<PhoneModel>> GetAllItemsAsync()
        {
            var phones = await context.Phones
                .ToListAsync();
            return phones;
        }

        public async Task<bool> AddPhoneAsync(PhoneModel newPhone)
        {
            newPhone.ID = Guid.NewGuid();
            context.Phones.Add(newPhone);

            var saveResult = await context.SaveChangesAsync();
            return saveResult == 1;
        }

        public async Task<bool> CreateDiscountAsync(PhoneModel newPhone)
        {
            var newPhones = new List<PhoneModel>();

            foreach (var item in context.Phones)
            {
                if (item.Brand == newPhone.Brand)
                {
                    item.Discount = newPhone.Discount;
                    newPhones.Add(item);
                    context.Phones.Remove(item);
                    context.SaveChanges();
                }
            }

            await context.Phones.AddRangeAsync(newPhones);

            var saveResult = await context.SaveChangesAsync();
            return saveResult >= 1;
        }

        public async Task<bool> DeletePhoneAsync(Guid id)
        {
            var product = await context.Phones
                .Where(x => x.ID == id)
                .AsNoTracking()
                .SingleOrDefaultAsync();

            context.Phones.Remove(product);
            var saveResult = await context.SaveChangesAsync();

            return saveResult == 1;
        }
    }
}
