using Aifitness.User.Service.Models;
using Aifitness.User.Service.Services.Workout;
using Microsoft.AspNetCore.Mvc;

namespace Aifitness_User_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkoutController : Controller
    {
        private IWorkoutService _workoutService;

        public WorkoutController(IWorkoutService workoutService)
        {
            _workoutService = workoutService;
        }

        [HttpGet("workouts/user/{userId}")]
        public List<WorkoutModel> GetWorkouts(string userId)
        {
            return _workoutService.GetWorkoutByUser(userId);
        }
        [HttpGet("workouts")]
        public List<WorkoutModel> GetWorkouts()
        {
            return _workoutService.GetWorkouts();
        }
        [HttpGet("workouts/{name}")]
        public List<WorkoutModel> GetWorkout(string name)
        {
            return _workoutService.GetWorkoutByName(name);
        }
        [HttpPost("createworkout")]
        public void CreateWorkout([FromBody] WorkoutModel workoutModel)
        {
            _workoutService.CreateWorkout(workoutModel);
        }
        [HttpPost("updateimage")]
        public void UpdateWorkout([FromBody] UpdateWorkouImageRequest updateWorkouImageRequest)
        {
            _workoutService.UpdateWorkoutImage(updateWorkouImageRequest);
        }
        [HttpPost("delete/{workoutId}")]
        public void DeleteWorkout(string workoutId)
        {
            _workoutService.DeleteWorkout(workoutId);
        }

    }
}
