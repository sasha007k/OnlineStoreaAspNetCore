using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Data;
using OnlineStore.Models;

namespace OnlineStore.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext dbContext = new ApplicationDbContext();

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> Phones(string searchString, string sortOrder)
        {
            var phones = from p in dbContext.Phones
                         select p;

            ViewData["BrandSortParm"] = String.IsNullOrEmpty(sortOrder) ? "BrandDesc" : "";
            ViewData["ModelSortParm"] = sortOrder == "Model" ? "ModelDesc" : "Model";
            ViewData["PriceSortParm"] = sortOrder == "Price" ? "PriceDesc" : "Price";

            switch (sortOrder)
            {
                case "BrandDesc":
                    phones = phones.OrderByDescending(p => p.Brand);
                    break;
                case "Model":
                    phones = phones.OrderBy(p => p.Model);
                    break;
                case "ModelDesc":
                    phones = phones.OrderByDescending(p => p.Model);
                    break;
                case "Price":
                    phones = phones.OrderBy(p => p.Price);
                    break;
                case "PriceDesc":
                    phones = phones.OrderByDescending(p => p.Price);
                    break;
                default:
                    phones = phones.OrderBy(p => p.Brand);
                    break;
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                phones = phones.Where(p => p.Brand.Contains(searchString)
                                       || p.Model.Contains(searchString));
            }

            
            return View(await phones.AsNoTracking().ToListAsync());
        }
    
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPhone(PhoneModel newPhone)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Phones");
            }

            newPhone.ID = Guid.NewGuid();
            dbContext.Phones.Add(newPhone);
            await dbContext.SaveChangesAsync();
            
            return RedirectToAction("Phones");
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePhone(Guid id)
        {
            if (id == Guid.Empty)
            {
                return RedirectToAction("Phones");
            }

            var product = await dbContext.Phones
                .Where(x => x.ID == id)
                .AsNoTracking()
                .SingleOrDefaultAsync();

            dbContext.Phones.Remove(product);
            await dbContext.SaveChangesAsync();

            return RedirectToAction("Phones");
        }        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
