using FaceRecogApp.Data;
using FaceRecogApp.Interfaces;
using FaceRecogApp.Models;
using FaceRecogApp.Dto;

namespace FaceRecogApp.Repositories
{
    public class ActivityRepository : IActivityRepository
    {
        private readonly DataContext _dataContext;
        public ActivityRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public ICollection<Activity> GetActivities()
        {
            return _dataContext.Activities.ToList();
        }

        public Activity GetActivity(int id)
        {
            return _dataContext.Activities.FirstOrDefault(a => a.ActivityId == id);
        }

        public bool Save()
        {
            var saved = _dataContext.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool ActivityExists(int activityId)
        {
            return _dataContext.Activities.Where(a => a.ActivityId == activityId).Any();
        }


        public bool CreateActivity(Activity activity)
        {
            _dataContext.Add(activity);

            return Save();
        }

        public bool DeleteActivity(Activity activity)
        {
            _dataContext.Remove(activity);

            return Save();
        }

        public bool UpdateActivity(Activity activity)
        {
            _dataContext.Update(activity);
            return Save();

        }

        public Activity GetActivityTrimToUpper(ActivityDto activityCreate)
        {
            return GetActivities().Where(c => c.Title.Trim().ToUpper() == activityCreate.Title.TrimEnd().ToUpper()).FirstOrDefault();
        }

        public Employee GetEmployeeByActivityId(int ActivityId)
        {
            return _dataContext.Activities.Where(a => a.ActivityId == ActivityId).Select(e => e.Organizer).FirstOrDefault();
        }
    }
}