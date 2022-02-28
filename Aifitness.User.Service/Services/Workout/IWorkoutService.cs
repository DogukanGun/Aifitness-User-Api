using Aifitness.User.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aifitness.User.Service.Services.Workout
{
    public interface IWorkoutService
    {
        public void DeleteWorkout(string workoutId);
        public void UpdateWorkoutImage(UpdateWorkouImageRequest updateWorkouImageRequest);
        public void CreateWorkout(WorkoutModel workoutModel);
        public List<WorkoutModel> GetWorkoutByName(string name);
        public List<WorkoutModel> GetWorkoutByUser(string userId);
        public List<WorkoutModel> GetWorkouts();

    }
}
