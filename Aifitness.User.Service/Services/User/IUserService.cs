using Aifitness.User.Service.Models;
using Aifitness_User_Api.Data.Documents;
using Aifitness_User_Api.Service.Models;
using System.Collections.Generic;

namespace Aifitness_User_Api.Service.Modules.User
{
    public interface IUserService
    {
        public void RegisterUser(UserRegisterModel userRegisterModel); 
        List<UserModel> GetUsers(List<string> usernames);
        UserModel GetLoggedInUser();
        bool DeleteUser();
        public void RegisterAsAdmin(RegisterAsAdminModel registerAsAdmin);
        public void RegisterAsTeacher(RegisterAsTeacherModel registerAsTeacherModel);
        public List<RegisterAsAdminModel> GetAllAdminRequest();
        public List<RegisterAsTeacherModel> GetAllTeacherRequests();
        public List<RegisterAsAdminModel> GetAllRejectedAdminRequest();
        public List<RegisterAsTeacherModel> GetAllRejectedTeacherRequests();
        public List<RegisterAsAdminModel> GetAllAcceptedAdminRequest();
        public List<RegisterAsTeacherModel> GetAllAcceptedTeacherRequests();
        public void ChangeAdminRequest(ChangeRequestStatus changeRequestStatus);
        public void ChangeTeacherRequest(ChangeRequestStatus changeRequestStatus);



    }
}