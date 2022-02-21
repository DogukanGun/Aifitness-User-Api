using Aifitness_User_Api.Data.Documents;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aifitness_User_Api.Data.Repositories
{
    public class LogOnAuditRepository : BaseMongoRepository<LogOnAuditDocument>, ILogOnAuditRepository
    {
        public LogOnAuditRepository(IOptions<MongoDbOptions> options) : base(options)
        {
        } 
    }
}
