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
using Aifitness.User.Service.Models;
using Aifitness.User.Data.Documents;
using Aifitness.User.Data.Repositories;

namespace Aifitness_User_Api.Service.Modules.User
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;
        private IMapper _mapper;
        private IRegisterAsTeacherRepository _registerAsTeacherRepository;
        private IRegisterAsAdminRepository _registerAsAdminRepository;
        readonly ISecurityService _securityService;
        readonly IContextResolver _contextResolver;

        public UserService(IUserRepository userRepository, IMapper mapper, ISecurityService securityService, IContextResolver contextResolver, IRegisterAsTeacherRepository registerAsTeacherRepository, IRegisterAsAdminRepository registerAsAdminRepository)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _securityService = securityService;
            _contextResolver = contextResolver;
            _registerAsTeacherRepository = registerAsTeacherRepository;
            _registerAsAdminRepository = registerAsAdminRepository;
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
        public void RegisterAsTeacher(RegisterAsTeacherModel registerAsTeacherModel)
        {
            RegisterAsTeacherDocument registerAsTeacherDocument = _mapper.Map<RegisterAsTeacherDocument>(registerAsTeacherModel);
            registerAsTeacherDocument.Status = UserStatus.PENDING;
            _registerAsTeacherRepository.InsertOne(registerAsTeacherDocument);
        }
        public void RegisterAsAdmin(RegisterAsAdminModel registerAsAdmin)
        {
            RegisterAsAdminDocument registerAsAdminDocument = _mapper.Map<RegisterAsAdminDocument>(registerAsAdmin);
            registerAsAdminDocument.Status = UserStatus.PENDING;
            _registerAsAdminRepository.InsertOne(registerAsAdminDocument);
        }
        public bool DeleteUser()
        {
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

        public void ChangeAdminRequest(ChangeRequestStatus changeRequestStatus)
        {
            List<RegisterAsAdminDocument> registerAsAdminDocumentList = _registerAsAdminRepository.FilterBy(x => x.Email == changeRequestStatus.Email && x.Status == UserStatus.PENDING).ToList();
            if(registerAsAdminDocumentList == null || registerAsAdminDocumentList.Count != 1)
            {
                throw new Exception();
            } 
            foreach (RegisterAsAdminDocument registerAsAdminDocument in registerAsAdminDocumentList)
            {
                registerAsAdminDocument.Status = changeRequestStatus.Status;
                _registerAsAdminRepository.ReplaceOne(registerAsAdminDocument);
                UserDocument userDocument = new UserDocument();
                userDocument.Username = registerAsAdminDocument.Name;
                userDocument.Email = registerAsAdminDocument.Email;
                userDocument.Name = registerAsAdminDocument.Name;
                userDocument.Surname = registerAsAdminDocument.Surname;
                userDocument.Status = changeRequestStatus.Status;

                byte[] salt = _securityService.GenerateRandomSalt();
                userDocument.Password = _securityService.GenerateHashedPassword(salt, registerAsAdminDocument.Password);
                userDocument.Salt = Convert.ToBase64String(salt);

                _userRepository.InsertOne(userDocument);
            }
        }

        public void ChangeTeacherRequest(ChangeRequestStatus changeRequestStatus)
        {
            List<RegisterAsTeacherDocument> registerAsTeacherDocumentList = _registerAsTeacherRepository.FilterBy(x => x.Email == changeRequestStatus.Email && x.Status == UserStatus.PENDING).ToList();
            if (registerAsTeacherDocumentList == null || registerAsTeacherDocumentList.Count != 1)
            {
                throw new Exception();
            }
            foreach (RegisterAsTeacherDocument registerAsTeacherDocument in registerAsTeacherDocumentList)
            {
                registerAsTeacherDocument.Status = changeRequestStatus.Status;
                _registerAsTeacherRepository.ReplaceOne(registerAsTeacherDocument);
                UserDocument userDocument = new UserDocument();
                userDocument.Username = registerAsTeacherDocument.Name;
                userDocument.Email = registerAsTeacherDocument.Email;
                userDocument.Name = registerAsTeacherDocument.Name;
                userDocument.Surname = registerAsTeacherDocument.Surname;
                userDocument.Status = changeRequestStatus.Status;

                byte[] salt = _securityService.GenerateRandomSalt();
                userDocument.Password = _securityService.GenerateHashedPassword(salt, registerAsTeacherDocument.Password);
                userDocument.Salt = Convert.ToBase64String(salt);

                _userRepository.InsertOne(userDocument);
            }
        }
        public List<RegisterAsAdminModel> GetAllRejectedAdminRequest()
        {
            return _mapper.Map<List<RegisterAsAdminModel>>(_registerAsAdminRepository.FilterBy(x => x.Status == UserStatus.INACTIVE));
        } 
        public List<RegisterAsTeacherModel> GetAllRejectedTeacherRequests()
        {
            return _mapper.Map<List<RegisterAsTeacherModel>>(_registerAsTeacherRepository.FilterBy(x => x.Status == UserStatus.INACTIVE));
        }
        public List<RegisterAsAdminModel> GetAllAcceptedAdminRequest()
        {
            return _mapper.Map<List<RegisterAsAdminModel>>(_registerAsAdminRepository.FilterBy(x => x.Status == UserStatus.APPROVED));
        }
        public List<RegisterAsTeacherModel> GetAllAcceptedTeacherRequests()
        {
            return _mapper.Map<List<RegisterAsTeacherModel>>(_registerAsTeacherRepository.FilterBy(x => x.Status == UserStatus.APPROVED));
        }

        public List<RegisterAsAdminModel> GetAllAdminRequest()
        {
            return _mapper.Map<List<RegisterAsAdminModel>>(_registerAsAdminRepository.FilterBy(x => x.Status == UserStatus.PENDING));
        }

        public List<RegisterAsTeacherModel> GetAllTeacherRequests()
        {
            return _mapper.Map<List<RegisterAsTeacherModel>>(_registerAsTeacherRepository.FilterBy(x => x.Status == UserStatus.PENDING));
        }
    }
}
