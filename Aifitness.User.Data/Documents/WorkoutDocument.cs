using Aifitness_User_Api.Data;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aifitness.User.Data.Documents
{
    [BsonCollection("workout")]
    public class WorkoutDocument: Document
    {
        public string WorkoutName { get; set; }
        public string WorkoutDescription { get; set; }
        public double WorkoutRating { get; set; }
        public double WorkoutEstimatedTime { get; set; }
        public string WorkoutLevel { get; set; }
        public string Image { get; set; }
        public bool IsDeleted { get; set; }
        public ObjectId UserId { get; set; }
    }
}
