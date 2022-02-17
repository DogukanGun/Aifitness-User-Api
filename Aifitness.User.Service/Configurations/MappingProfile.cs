using Aifitness_User_Api.Data.Documents;
using Aifitness_User_Api.Service.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aifitness_User_Api.Service.Configurations
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<UserModel, UserDocument>();
            CreateMap<LogOnAuditModel,LogOnAuditDocument>();
        }
    }
}
