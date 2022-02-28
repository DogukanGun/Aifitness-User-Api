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
    public class WorkoutRepository : BaseMongoRepository<WorkoutDocument>, IWorkoutRepository
    {
        public WorkoutRepository(IOptions<MongoDbOptions> options) : base(options)
        {
        }
    }
}
