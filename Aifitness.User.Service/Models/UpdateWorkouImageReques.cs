using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aifitness.User.Service.Models
{
    public class UpdateWorkouImageRequest
    {
        public string WorkoutId { get; set; }
        public string WorkoutImage { get; set; }
    }
}
