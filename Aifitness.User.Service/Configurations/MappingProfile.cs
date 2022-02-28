using Aifitness.User.Data.Documents;
using Aifitness.User.Service.Models;
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
            CreateMap<UserDocument, UserModel>();
            CreateMap<RegisterAsAdminModel, RegisterAsAdminDocument>();
            CreateMap<RegisterAsAdminDocument, RegisterAsAdminModel>();
            CreateMap<RegisterAsTeacherDocument, RegisterAsTeacherModel>();
            CreateMap<RegisterAsTeacherModel, RegisterAsTeacherDocument>();
            CreateMap<LogOnAuditModel,LogOnAuditDocument>();
            CreateMap<LogOnAuditDocument, LogOnAuditDocument>();
            CreateMap<WorkoutDocument, WorkoutModel>();
            CreateMap<WorkoutModel, WorkoutDocument>();
        }
    }
}
