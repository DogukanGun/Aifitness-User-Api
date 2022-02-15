using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aifitness_User_Api.Service
{
    public interface IContextResolver
    {
        public string GetUsername();
        public string GetIPAddress();
        public string GetWebBrowser();
        public string GetDate();
    }
}
