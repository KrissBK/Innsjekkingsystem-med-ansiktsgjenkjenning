using FaceRecogApp.Models;

namespace FaceRecogApp.Interfaces
{
    public interface IActivityRepository 
    {
        ICollection<Activity> GetActivities();

        Activity GetActivity(int id);

        bool ActivityExists(int activityId);

        bool CreateActivity(Activity activity);

        bool DeleteActivity(Activity activity);

        bool UpdateActivity(Activity activity);

        Employee GetEmployeeByActivityId(int activityId);

        bool Save();
    }
}
