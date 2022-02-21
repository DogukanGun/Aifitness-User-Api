using Aifitness.User.Service.Models;
using Aifitness_User_Api.Service.Models;
using Aifitness_User_Api.Service.Modules.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;


namespace Aifitness_User_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;
 
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/<UserController>
        [HttpGet]
        public UserModel Get()
        {
            return _userService.GetLoggedInUser();
        }

        // POST api/<UserController>
        [HttpPost("register")]
        public void RegisterUser([FromBody] UserRegisterModel userRegisterModel)
        {
            _userService.RegisterUser(userRegisterModel);
        }

        [HttpPost("registerasteacher")]
        public void BecomeTeacher([FromBody] RegisterAsTeacherModel registerAsTeacherModel)
        {
            _userService.RegisterAsTeacher(registerAsTeacherModel);
        }

        [HttpPost("registerasadmin")]
        public void BecomeAdmin([FromBody] RegisterAsAdminModel registerAsAdmin)
        {
            _userService.RegisterAsAdmin(registerAsAdmin);
        }

        [HttpGet("teacherequests/pending")]
        public List<RegisterAsTeacherModel> GetAllTeacherRequests()
        {
            return _userService.GetAllTeacherRequests();
        }

        [HttpGet("adminrequests/pending")]
        public List<RegisterAsAdminModel> GetAllAdminRequests()
        {
            return _userService.GetAllAdminRequest();
        }

        [HttpGet("adminrequests/approved")]
        public List<RegisterAsAdminModel> GetAllAcceptedAdminRequests()
        {
            return _userService.GetAllAcceptedAdminRequest();
        }

        [HttpGet("teacherrequests/approved")]
        public List<RegisterAsTeacherModel> GetAllAcceptedTeacherRequests()
        {
            return _userService.GetAllAcceptedTeacherRequests();
        }

        [HttpGet("teacherrequests/rejected")]
        public List<RegisterAsTeacherModel> GetAllRejectedTeacherRequests()
        {
            return _userService.GetAllRejectedTeacherRequests();
        }

        [HttpGet("adminrequests/rejected")]
        public List<RegisterAsAdminModel> GetAllRejectedAdminRequests()
        {
            return _userService.GetAllRejectedAdminRequest();
        }
        [HttpPost("changerequest/teacher")]
        public void ChangeTeacherRequest([FromBody] ChangeRequestStatus changeRequestStatus)
        {
            _userService.ChangeTeacherRequest(changeRequestStatus);
        }
        [HttpPost("changerequest/admin")]
        public void ChangeAdminRequest([FromBody] ChangeRequestStatus changeRequestStatus)
        {
            _userService.ChangeAdminRequest(changeRequestStatus);
        } 
        [HttpDelete]
        public void DeleteUser()
        {
            _userService.DeleteUser();
        } 
    }
}
