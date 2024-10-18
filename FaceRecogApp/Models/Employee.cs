namespace FaceRecogApp.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool Attendance { get; set; }

        public JobTitle JobTitle { get; set; }

        public Guid faceServicePersonId { get; set; }

        public DateTime? latestCheckIn { get; set; }

        public ICollection<Picture> Pictures { get; set; }

        public ICollection<Activity> OrganizedActivities { get; set; }


    }
}
