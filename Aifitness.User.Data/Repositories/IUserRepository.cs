using Aifitness_User_Api.Data.Abstraction;
using Aifitness_User_Api.Data.Documents;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aifitness_User_Api.Data.Repositories
{
    public interface IUserRepository : IMongoRepository<UserDocument>
    {

    }
}
