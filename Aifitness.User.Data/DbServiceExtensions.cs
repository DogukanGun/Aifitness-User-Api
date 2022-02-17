
using Aifitness_User_Api.Data.Repositories;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DbServiceExtensions
    {
        public static void AddDbServices(this IServiceCollection services)
        {
            services.AddSingleton<ILogOnAuditRepository, LogOnAuditRepository>(); 
            services.AddSingleton<IUserRepository, UserRepository>();              
        }
    }
}
