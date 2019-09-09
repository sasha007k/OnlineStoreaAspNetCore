using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Services;
using OnlineStore.Models;
using Microsoft.AspNetCore.Authorization;

namespace OnlineStore.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {
        private readonly IPhoneModelService phoneModelService;        

        public HomeController(IPhoneModelService phoneModelService)
        {
            this.phoneModelService = phoneModelService;
        }       

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Privacy()
        {
            return View();
        }

        [AllowAnonymous]
        public async Task<IActionResult> Phones(string searchString, string sortOrder, string currentFilter, int? pageNumber)
        {
            var phones = await phoneModelService.GetAllItemsAsync();

            ViewData["CurrentSort"] = sortOrder;
            ViewData["BrandSortParm"] = String.IsNullOrEmpty(sortOrder) ? "BrandDesc" : "";
            ViewData["ModelSortParm"] = sortOrder == "Model" ? "ModelDesc" : "Model";
            ViewData["PriceSortParm"] = sortOrder == "Price" ? "PriceDesc" : "Price";

            if(searchString != null)
    {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewData["CurrentFilter"] = searchString;

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


            int pageSize = 4;
            return View(await PaginatedList<PhoneModel>.CreateAsync(phones, pageNumber ?? 1, pageSize));
        }
    
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AddPhone(PhoneModel newPhone)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Phones");
            }

            var successful = await phoneModelService.AddPhoneAsync(newPhone);
            if (!successful)
            {
                return BadRequest("Could not add phone.");
            }

            return RedirectToAction("Phones");
        }

        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeletePhone(Guid id)
        {
            if (id == Guid.Empty)
            {
                return RedirectToAction("Phones");
            }

            var successful = await phoneModelService.DeletePhoneAsync(id);
            if (!successful)
            {
                return BadRequest("Could not delete this phone.");
            }

            return RedirectToAction("Phones");
        }        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }        
    }
}
