using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aifitness_User_Api.Data.Documents
{
    [BsonCollection("logonaudit")]
    public class LogOnAuditDocument:Document
    {
        public string LoginDate { get; set; }
        public string IpAddress { get; set; }
        public string WebBrowser { get; set; }
        public int LoginStatus { get; set; }
        public string LoginStatusDescription { get; set; }
        public string Username { get; set; }
    }
}
