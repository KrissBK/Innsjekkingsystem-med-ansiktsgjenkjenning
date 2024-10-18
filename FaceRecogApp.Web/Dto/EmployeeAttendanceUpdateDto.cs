namespace FaceRecogApp.Web.Dto
{
    public class EmployeeAttendanceUpdateDto
    {
        public int EmployeeId { get; set; }

        public bool Attendance { get; set; }

        public DateTime latestCheckIn { get; set; }
    }
}
