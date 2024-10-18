using FaceRecogApp.Models;

namespace FaceRecogApp.Interfaces
{
    public interface IJobTitleRepository
    {
        ICollection<JobTitle> GetJobTitles();

        JobTitle GetJobTitle(int jobTitleId);

        bool JobTitleExists(int jobTitleId);
    }
}
