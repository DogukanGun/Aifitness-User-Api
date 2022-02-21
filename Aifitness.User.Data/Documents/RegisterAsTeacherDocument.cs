using Aifitness_User_Api.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aifitness.User.Data.Documents
{
    [BsonCollection("teacher_request")]
    public class RegisterAsTeacherDocument: Document
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Status { get; set; }
        public string Password { get; set; }

    }
}
