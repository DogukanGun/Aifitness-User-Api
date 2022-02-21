using Aifitness.User.Data.Documents;
using Aifitness_User_Api.Data;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aifitness.User.Data.Repositories
{
    public class RegisterAsAdminRepository : BaseMongoRepository<RegisterAsAdminDocument>, IRegisterAsAdminRepository
    {
        public RegisterAsAdminRepository(IOptions<MongoDbOptions> options) : base(options)
        {
        }
    }
}
