using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.Middlewares
{
    public class PreventLoginIfAuthenticatedMiddleware
    {
        private readonly RequestDelegate _requestDelegate;
        public PreventLoginIfAuthenticatedMiddleware(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            if(context.User.Identity.IsAuthenticated)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                await context.Response.WriteAsync("Already authenticated.");
                return;

            }
           await _requestDelegate(context);
        }
    }
}
