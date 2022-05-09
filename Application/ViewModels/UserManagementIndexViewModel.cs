using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Text;
using Domain.Models;

namespace Application.ViewModels
{
    public class UserManagementIndexViewModel
    {
        public List<IdentityUser> Users { get; set; }
    }
}
