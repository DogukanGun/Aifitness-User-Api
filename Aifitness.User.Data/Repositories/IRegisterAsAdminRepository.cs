using Aifitness.User.Data.Documents;
using Aifitness_User_Api.Data.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aifitness.User.Data.Repositories
{
    public interface IRegisterAsAdminRepository : IMongoRepository<RegisterAsAdminDocument>
    {
    }
}
