using Aifitness_User_Api.Service.Configurations;
using System.Collections.Generic;

namespace Aifitness_User_Api.Service.Modules.Authentication
{
    public interface ISecurityService
    {
        public string GeneratePassword();
        public byte[] GenerateRandomSalt();
        public string GenerateHashedPassword(byte[] salt, string password);
        public string GenerateToken(JwtOptions jwtOptions, string username);
     }
}
