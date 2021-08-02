using System;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ServerApp.Helpers.Enseignant
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class EnseignantAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = (EspEnseignant) context.HttpContext.Items["Enseignant"];

            if (user == null)
            {
                // not logged in
                context.Result =
                    new JsonResult(new { message = "Unauthorized" })
                    { StatusCode = StatusCodes.Status401Unauthorized };
            }
        }
    }
}