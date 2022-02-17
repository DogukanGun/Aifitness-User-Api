using Aifitness_User_Api.Service.Configurations;
using Aifitness_User_Api.Service.Modules.Authentication;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Aifitness_User_Api.Service.Modules.Authentication
{
    public class SecurityService : ISecurityService
    {
        public string GeneratePassword()
        {
            StringBuilder password = new StringBuilder();
            Random random = new Random();
            int iteration = 0;

            while (iteration < 3)
            {
                char numeric = (char)random.Next(48, 57);
                char uppercase = (char)random.Next(65, 90);
                char lowercase = (char)random.Next(97, 122);
                password.Append(numeric);
                password.Append(uppercase);
                password.Append(lowercase);
                iteration++;
            }

            return password.ToString();
        }

        public byte[] GenerateRandomSalt()
        {
            // generate a 128-bit salt using a secure PRNG
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }

        public string GenerateHashedPassword(byte[] salt, string password)
        {
            // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
            string hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
            return hashedPassword;
        }
 
        public string GenerateToken(JwtOptions jwtOptions, string username)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(jwtOptions.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("username", username)
                }),
                Issuer = jwtOptions.Issuer,
                Audience = jwtOptions.Issuer,
                Expires = DateTime.UtcNow.AddHours(100),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
