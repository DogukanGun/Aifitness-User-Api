
using Aifitness_User_Api.Service;
using Microsoft.AspNetCore.Http;
using System;

namespace Aifitness_User_Api.Controllers

{
    public class HttpContextResolver : IContextResolver
    {
        private readonly IHttpContextAccessor _context;

        public HttpContextResolver(IHttpContextAccessor context)
        {
            _context = context;
        }

        public string GetUsername()
        {
            return _context.HttpContext.User.FindFirst("username").Value;
        }

        public string GetLanguage()
        {
            return _context.HttpContext.Request.Headers["language"].ToString();
        }
        public string GetIPAddress()
        {
            return _context.HttpContext.Connection.LocalIpAddress.ToString();
        }
        public string GetWebBrowser()
        {
            return _context.HttpContext.Request.Headers["User-Agent"].ToString();
        }
        public string GetDate()
        {
            return DateTime.Now.ToString();
        }

    }
}
