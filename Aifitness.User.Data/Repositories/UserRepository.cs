using Aifitness_User_Api.Data.Documents;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aifitness_User_Api.Data.Repositories
{
    public class UserRepository : BaseMongoRepository<UserDocument>, IUserRepository
    {
        public UserRepository(IOptions<MongoDbOptions> options) : base(options)
        {

        }
    }
}
