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
    }
}