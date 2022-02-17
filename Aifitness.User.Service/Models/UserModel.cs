using System;
using System.Collections.Generic;
using System.Text;

namespace Aifitness_User_Api.Service.Models
{
    public class UserModel
    {
        public bool IsTeacher { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public string Salt { get; set; }
        public string Branch { get; set; }
        public string Class { get; set; }
        public DateTime BornDate { get; set; }
        public string Email { get; set; }
        public bool IsDeleted { get; set; }
        public string Status { get; set; }
    }
}
