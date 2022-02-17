using Aifitness_User_Api.Service.Models;
using Aifitness_User_Api.Service.Modules.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aifitness_User_Api.Controllers
{
    [Route("api")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private IAuthenticationService _authService;
        private ILogOnAuditService _logOnAuditService;


        public AuthenticationController(IAuthenticationService authService, ILogOnAuditService logOnAuditService)
        {
            _authService = authService;
            _logOnAuditService = logOnAuditService;
        }

        [Route("auth/user/login")]
        [HttpPost]
        public IActionResult Login(LoginModel loginModel)
        {

            return Ok(new { token = _authService.Authenticate(loginModel.EmailAddress, loginModel.Password) });
        }



        [Route("auth/user/login-log")]
        [HttpGet]
        public List<LogOnAuditModel> GetLoginLogs()
        {
            return _logOnAuditService.GetLogins();
        }

        [Route("password/reset")]
        [HttpPost]
        public IActionResult ResetPassword(string username)
        {
            _authService.ResetPassword(username);
            return Ok();
        }

        [Route("password/change")]
        [HttpPost]
        [Authorize]
        public bool ChangePassword(PasswordChangeRequest passwordChangeRequest)
        {
            return _authService.ChangePassword(passwordChangeRequest.CurrentPassword, passwordChangeRequest.NewPassword);
        }
    }

    public class LoginModel
    {
        public string EmailAddress { get; set; }
        public string Password { get; set; }
    }
}
