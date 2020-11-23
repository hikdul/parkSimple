using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using parking.Models;
using Parking.helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace parking.Helpers
{
    public class RolesUsuario : IActionFilter
    {
        //antes
        public void OnActionExecuted(ActionExecutedContext context)
        {
            //throw new NotImplementedException();
        }
        //despues
        public void OnActionExecuting(ActionExecutingContext context)
        {
            string bandera = context.HttpContext.Session.GetString("usuario");

            if (String.IsNullOrEmpty(bandera))
            {
                context.Result = new RedirectResult("/Login/Login");
            }
            else
            {
                int rol = (int)context.HttpContext.Session.GetInt32("rol");

                //roles 1=> publico || 2=> cobrador || 3=>  administrdos

            }

        }
    }
}
