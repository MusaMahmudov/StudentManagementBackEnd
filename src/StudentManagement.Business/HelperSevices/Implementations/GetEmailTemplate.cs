using StudentManagement.Business.HelperSevices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using StudentManagement.Business.DTOs.AuthDTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Routing;
using System.Net.Http;
using static System.Net.WebRequestMethods;
using System.Security.Policy;

namespace StudentManagement.Business.HelperSevices.Implementations
{
    public class GetEmailTemplate : IGetEmailTemplate
    {

        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IUrlHelper _urlHelper;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly LinkGenerator _linkGenerator;
        public GetEmailTemplate(LinkGenerator linkGenerator,IHttpContextAccessor contextAccessor,IUrlHelper urlHelper,IWebHostEnvironment webHostEnvironment)
        {
            _linkGenerator = linkGenerator;
            _contextAccessor = contextAccessor;
            _urlHelper = urlHelper;
            _webHostEnvironment = webHostEnvironment;

        }
        public async Task<string> GetResetPasswordTemplateAsync(string token,string email)
        {
            string path = Path.Combine(_webHostEnvironment.WebRootPath,"assets","Templates","reset-password.html");
            StreamReader str = new StreamReader(path);
            string result = await str.ReadToEndAsync();
            //var link = _linkGenerator.GetUriByAction(_contextAccessor.HttpContext, action: "ResetPassword",
            //controller: "Auth",
            //values: new { token = token, email = email });
            var myHost = "localhost:3000";

            HostString myHostResult = new HostString(myHost);

            
            var link = _linkGenerator.GetUriByPage(_contextAccessor.HttpContext, page: "/SignIn", values: new { token = token, email=email }, host: myHostResult);
            return result.Replace("[link]", link);
        }
    }
}
