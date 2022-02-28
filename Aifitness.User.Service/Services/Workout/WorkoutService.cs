using Aifitness.User.Data.Documents;
using Aifitness.User.Data.Repositories;
using Aifitness.User.Service.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aifitness.User.Service.Services.Workout
{
    public class WorkoutService : IWorkoutService
    {
        private IWorkoutRepository _workoutRepository;
        private readonly IMapper _mapper;

        public WorkoutService(IWorkoutRepository workoutRepository,IMapper mapper)
        {
            _workoutRepository = workoutRepository;
            _mapper = mapper;
        }
        public List<WorkoutModel> GetWorkouts()
        {
            return _mapper.Map<List<WorkoutModel>>(_workoutRepository.FilterBy(x => x.IsDeleted == false));
        }
        public List<WorkoutModel> GetWorkoutByUser(string userId)
        {
            return _mapper.Map<List<WorkoutModel>>(_workoutRepository.FilterBy(x => x.UserId.ToString() == userId && x.IsDeleted == false));
        }
        public List<WorkoutModel> GetWorkoutByName(string name)
        {
            return _mapper.Map<List<WorkoutModel>>(_workoutRepository.FilterBy(x=>x.WorkoutName.Contains(name) && x.IsDeleted == false));
        }
        public void CreateWorkout(WorkoutModel workoutModel)
        {
            WorkoutDocument workoutDocument = _mapper.Map<WorkoutDocument>(workoutModel);
            _workoutRepository.InsertOne(workoutDocument);
        }
        public void DeleteWorkout(string workoutId)
        {
            WorkoutDocument workoutDocument = _workoutRepository.FilterBy(x => x.Id.ToString() == workoutId && x.IsDeleted == false).First();
            workoutDocument.IsDeleted = true;
            _workoutRepository.ReplaceOne(workoutDocument);
        }
        public void UpdateWorkoutImage(UpdateWorkouImageRequest updateWorkouImageRequest)
        {
            WorkoutDocument workoutDocument = _workoutRepository.FilterBy(x => x.Id.ToString() == updateWorkouImageRequest.WorkoutId && x.IsDeleted == false).First();
            workoutDocument.Image = updateWorkouImageRequest.WorkoutImage;
            _workoutRepository.ReplaceOne(workoutDocument);
        }
    }
}
