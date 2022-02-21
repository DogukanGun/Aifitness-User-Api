using Aifitness_User_Api.Data.Abstraction;
using Aifitness_User_Api.Data.Documents;
using System;
using System.Collections.Generic;
using System.Threading.Tasks; 

namespace Aifitness_User_Api.Data.Repositories
{
    public interface ILogOnAuditRepository : IMongoRepository<LogOnAuditDocument>
    {
    }
}
