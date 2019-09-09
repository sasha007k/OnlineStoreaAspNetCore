using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Data;
using OnlineStore.Models;
using OnlineStore.Services;

namespace OnlineStore.Controllers
{
    public class UserShowController : Controller
    {
        private readonly IUserModelService userModelService;

        public UserShowController(IUserModelService userModelService)
        {
            this.userModelService = userModelService;
        }


        [Authorize(Roles = "admin")]
        public async Task<IActionResult> ShowAllUsers(RoleModel role)
        {
            var users = await userModelService.GetAllItemsAsync();

            var model = new RoleModel()
            {
                Users = users
            };

            return View(model);
        }
    }
}
