using System;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ServerApp.Helpers.Parent
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ParentAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = (EspEtudiant) context.HttpContext.Items["Parent"];

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