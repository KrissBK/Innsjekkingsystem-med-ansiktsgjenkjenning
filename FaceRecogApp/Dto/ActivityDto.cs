namespace FaceRecogApp.Dto
{
    public class ActivityDto
    {
        public int ActivityId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public string Location { get; set; }

        public EmployeeDto? Organizer { get; set; }
    }
}
