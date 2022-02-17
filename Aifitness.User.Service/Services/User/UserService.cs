using AutoMapper; 
using Aifitness_User_Api.Data.Documents;
using Aifitness_User_Api.Data.Repositories;
using Aifitness_User_Api.Service;
using Aifitness_User_Api.Service.Constants;
using Aifitness_User_Api.Service.Models;
using Aifitness_User_Api.Service.Modules.Authentication;
using Aifitness_User_Api.Service.Modules.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Aifitness_User_Api.Service.Modules.User
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;
        private IMapper _mapper;
        readonly ISecurityService _securityService;
        readonly IContextResolver _contextResolver;

        public UserService(IUserRepository userRepository, IMapper mapper, ISecurityService securityService, IContextResolver contextResolver)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _securityService = securityService;
            _contextResolver = contextResolver;
        }
 
 
        public UserModel GetLoggedInUser()
        {
            UserDocument user = _userRepository.FindOne(x => x.Username == _contextResolver.GetUsername());
            return _mapper.Map<UserModel>(user);
        } 

        public List<UserModel> GetUsers(List<string> usernames)
        {
            return _mapper.Map<List<UserModel>>(_userRepository.FilterBy(x => usernames.Contains(x.Username))); 
        }
        public bool DeleteUser()
        {
            //_userRepository.DeleteOne(x => x.Username == _contextResolver.GetUsername());
            UserDocument user = _userRepository.FindOne(x => x.Username == _contextResolver.GetUsername());
            if(user != null && user.IsDeleted == false)
            {
                user.IsDeleted = true;
                _userRepository.ReplaceOne(user);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void RegisterUser(UserRegisterModel userRegisterModel)
        {
            UserDocument user = new UserDocument();
            user.Name = userRegisterModel.Name;
            user.Surname = userRegisterModel.Surname;
            user.Email = userRegisterModel.EmailAddress;
            user.Username = user.Email;
            user.Status = UserStatus.ACTIVE; //TODO: mail atmadan önce Pending olması gerek

            byte[] salt = _securityService.GenerateRandomSalt();
            user.Password = _securityService.GenerateHashedPassword(salt, userRegisterModel.Password);
            user.Salt = Convert.ToBase64String(salt);

            _userRepository.InsertOne(user);

            //TODO: aktifleştirme için mail at
        }
    }
}
