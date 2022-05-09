using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Presentation.Models;
using Microsoft.AspNetCore.Routing;

namespace Presentation.ActionFilters
{
    public class CustomAuthorization : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string username = context.HttpContext.User.Identity.Name;

            string id = context.HttpContext.Request.Query["id"];

            if (string.IsNullOrEmpty(id)) context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { action = "ErrorMessage", controller = "Home" , message="access denied"  }));
            else
            {
                //validate whether username is assocated with id
                //assuming that in the db you have allocated permissons between users and files

                //if(check is not ok) return new UnauthorizedResult();
            }

            //if (username != "ryanattard@gmail.com")
            //    context.Result = new UnauthorizedResult();
                //throw new UnauthorizedAccessException();
        }
        
    }
}
