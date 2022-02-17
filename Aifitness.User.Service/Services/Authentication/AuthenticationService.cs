using Aifitness_User_Api.Data.Documents;
using Aifitness_User_Api.Data.Repositories;
using Aifitness_User_Api.Service.Configurations;
using Aifitness_User_Api.Service.Constants;
using Aifitness_User_Api.Service.Exceptions;
using Aifitness_User_Api.Service.Modules.Authentication;
using Microsoft.Extensions.Options;
using System;
using System.Linq.Expressions;

namespace Aifitness_User_Api.Service.Modules.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        readonly ISecurityService _securityService;
        readonly IUserRepository _userRepository;
        readonly JwtOptions _jwtOptions;
        readonly IContextResolver _contextResolver;
        readonly ILogOnAuditService _logOnAuditService; 
         
        public AuthenticationService(IContextResolver contextResolver,ISecurityService securityService, IUserRepository userRepository,ILogOnAuditService logOnAuditService, IOptions<JwtOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
            _securityService = securityService;
            _userRepository = userRepository;
            _contextResolver = contextResolver;
            _logOnAuditService = logOnAuditService;
        }

        public string Authenticate(string userId, string password)
        {
            UserDocument loggedinUser = _userRepository.FindOne(x => x.Email == userId);
 
            if (loggedinUser == null)
            { 
                throw new BusinessException("Kullanıcı bulunamadı");
            }
            if(loggedinUser.IsDeleted == true)
            { 
                throw new BusinessException("Kullanıcı bulunamadı");
            }
            if (loggedinUser.Status == UserStatus.INACTIVE)
            {
                 throw new BusinessException("Bu kullanıcı giriş yapamaz. Lütfen sistem yöneticisiyle görüşün.");
            }
            if (loggedinUser.Status == UserStatus.PENDING)
            {
                 throw new BusinessException("Üyeliğiniz henüz onaylanmamış. Lütfen sistem yöneticisiyle görüşün.");
            }

            string hashedPassword = _securityService.GenerateHashedPassword(Convert.FromBase64String(loggedinUser.Salt), password);

            if (loggedinUser.Password != hashedPassword)
            {
                _logOnAuditService.SaveFailedLogin("Kullanıcı adınız veya şifreniz yanlış!",userId);
                throw new BusinessException("Kullanıcı adınız veya şifreniz yanlış!");
            }

            _logOnAuditService.SaveSuccessfulLogin(userId);
            return _securityService.GenerateToken(_jwtOptions, userId);
        }

        public void ResetPassword(string username)
        {

        }

        public bool ChangePassword(string password, string newPassword)
        {
            var username = _contextResolver.GetUsername();
            Expression<Func<UserDocument, bool>> getCurrentUser = x => x.Username == username;
            var currentUser = _userRepository.FindOne(getCurrentUser);
            if(currentUser != null)
            {
                byte[] salt = Convert.FromBase64String(currentUser.Salt);
                password = _securityService.GenerateHashedPassword(salt, password);
                if (password == currentUser.Password)
                {
                    newPassword = _securityService.GenerateHashedPassword(salt, newPassword);
                    currentUser.Password = newPassword;
                    _userRepository.ReplaceOne(currentUser);
                    return true;

                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            

        }

    }
}
