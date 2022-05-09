using DataAccess.Context;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Configuration
{
    public class UserRoleSeed
    {

        private readonly RoleManager<IdentityRole> _roleManager;
       public UserRoleSeed(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async void Seed()
        {
            if ((await _roleManager.FindByNameAsync("User")) == null)
            {
               await _roleManager.CreateAsync(new IdentityRole { Name = "User" });
            }

            if ((await _roleManager.FindByNameAsync("Admin")) == null)
            {
                await _roleManager.CreateAsync(new IdentityRole { Name = "Admin" }); 
            }
            return;

        }
    }
}
