using AutoMapper; 
using Aifitness_User_Api.Data.Documents;
using Aifitness_User_Api.Data.Repositories;
using Aifitness_User_Api.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aifitness_User_Api.Service.Modules.Authentication
{
    public class LogOnAuditService:ILogOnAuditService
    {
        readonly ILogOnAuditRepository _logAndAuditRepository;
        readonly IContextResolver _contextResolver;
        readonly IMapper _mapper;


        public LogOnAuditService(ILogOnAuditRepository logAndAuditRepository,IContextResolver contextResolver,IMapper mapper)
        {
            _logAndAuditRepository = logAndAuditRepository;
            _contextResolver = contextResolver;
            _mapper = mapper;
        }

        public void SaveFailedLogin(string status,string name)
        {
            LogOnAuditDocument logOnAuditDocument = new LogOnAuditDocument();
            logOnAuditDocument.IpAddress = _contextResolver.GetIPAddress();
            logOnAuditDocument.LoginDate = _contextResolver.GetDate();
            logOnAuditDocument.LoginStatus = 0;
            logOnAuditDocument.Username = name;
            logOnAuditDocument.LoginStatusDescription = status;
            logOnAuditDocument.WebBrowser = _contextResolver.GetWebBrowser();
            _logAndAuditRepository.InsertOne(logOnAuditDocument);
        }
         
        public void SaveSuccessfulLogin(string name)
        {
            LogOnAuditDocument logOnAuditDocument = new LogOnAuditDocument();
            logOnAuditDocument.IpAddress = _contextResolver.GetIPAddress();
            logOnAuditDocument.LoginDate = _contextResolver.GetDate();
            logOnAuditDocument.LoginStatus = 1;
            logOnAuditDocument.Username = name;
            logOnAuditDocument.LoginStatusDescription = "";
            logOnAuditDocument.WebBrowser = _contextResolver.GetWebBrowser();
            _logAndAuditRepository.InsertOne(logOnAuditDocument);
        }
 

        List<LogOnAuditModel> ILogOnAuditService.GetLogins()
        {
            return _mapper.Map<List<LogOnAuditModel>>(_logAndAuditRepository.FilterBy(x => x.Username == _contextResolver.GetUsername()).ToList());
        }
    }
}
