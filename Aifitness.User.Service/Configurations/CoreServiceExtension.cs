using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks; 
using AutoMapper;
using Aifitness_User_Api.Data;
using Aifitness_User_Api.Service.Modules.Authentication;
using Aifitness_User_Api.Service.Modules.User;

namespace Aifitness_User_Api.Service.Configurations
{
    public static class CoreServiceExtensions
    {
        public static void AddCoreService(this IServiceCollection services, Action<MongoDbOptions> dbOptions, Action<JwtOptions> jwtOptions)
        {
            if (dbOptions == null)
            {
                throw new ArgumentNullException(nameof(dbOptions),
                    @"Please provide options for MyService.");
            }

            if (jwtOptions == null)
            {
                throw new ArgumentNullException(nameof(jwtOptions),
                    @"Please provide options for MyService.");
            }

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });


            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.Configure(dbOptions);
            services.Configure(jwtOptions);

            services.AddSingleton<IAuthenticationService, AuthenticationService>();
            services.AddSingleton<ILogOnAuditService, LogOnAuditService>();
            services.AddSingleton<ISecurityService, SecurityService>();
            services.AddSingleton<IUserService, UserService>();
            services.AddDbServices();
        }
    }

    public class CoreServiceOptions
    {
        public MongoDbOptions MongoDbOptions { get; set; }
        public JwtOptions JwtOptions { get; set; }
    }
}
