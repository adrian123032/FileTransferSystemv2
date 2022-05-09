using Application.ViewModels;
using DataAccess.Context;
using DataAccess.Repositories;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserManagementController: Controller
    {
        private readonly FileTransferContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserManagementController(FileTransferContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;

        }


        public IActionResult Index()
        {
            
            var user = _roleManager.FindByIdAsync("ad28523ff1ef");

            var vm = new UserManagementIndexViewModel
            {
                Users = _context.Users.OrderBy(u => u.Email).ToList()

            };
          
            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> AddRole(string Id)
        {
            var user = await _userManager.FindByIdAsync(Id);
            var vm = new UserManagementAddRoleViewModel
            {
                UserId = Id,
                Email = user.Email
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> AddRole(UserManagementAddRoleViewModel rvm)
        {
            var user = await _userManager.FindByIdAsync(rvm.UserId);
            if (ModelState.IsValid)
            {
                
                var result = await _userManager.AddToRoleAsync(user, rvm.NewRole);
                if(result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
                return View(rvm);

            }
            rvm.Email = user.Email;
            return View(rvm);
        }

    }
}
