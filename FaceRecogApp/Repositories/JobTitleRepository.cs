using FaceRecogApp.Data;
using FaceRecogApp.Interfaces;
using FaceRecogApp.Models;

namespace FaceRecogApp.Repositories
{
    public class JobTitleRepository : IJobTitleRepository
    {
        private readonly DataContext _dataContext;

        public JobTitleRepository(DataContext dataContext) {
            _dataContext = dataContext;
        }
        public ICollection<JobTitle> GetJobTitles()
        {
            return _dataContext.JobTitles.OrderBy(j => j.JobTitleId).ToList();
        }
        public JobTitle GetJobTitle(int jobTitleId)
        {
            return _dataContext.JobTitles.Where(j => j.JobTitleId == jobTitleId).FirstOrDefault();
        }

        public bool JobTitleExists(int jobTitleId)
        {
            return _dataContext.JobTitles.Where(j => j.JobTitleId == jobTitleId).Any();
        }
    }
}
