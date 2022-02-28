using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aifitness.User.Service.Models
{
    public class WorkoutModel
    {
        public string WorkoutName { get; set; }
        public string WorkoutDescription { get; set; }
        public double WorkoutRating { get; set; }
        public double WorkoutEstimatedTime { get; set; }
        public string WorkoutLevel { get; set; }
        public string Image { get; set; }
    }
}
