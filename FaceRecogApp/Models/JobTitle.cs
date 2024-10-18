namespace FaceRecogApp.Models
{
    public class JobTitle
    {
        public int JobTitleId { get; set; } 

        public string Title { get; set; }

        public ICollection<Employee> Employees { get; set; }


    }
}
