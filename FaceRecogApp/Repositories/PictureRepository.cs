using FaceRecogApp.Data;
using FaceRecogApp.Interfaces;
using FaceRecogApp.Models;

namespace FaceRecogApp.Repositories
{
    public class PictureRepository : IPictureRepository
    {
        private readonly DataContext _dataContext;

        public PictureRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public bool CreatePicture(Picture picture)
        {
            _dataContext.Add(picture);

            return Save();
        }

        public bool DeletePictue(Picture picture)
        {
            _dataContext.Remove(picture);

            return Save();
        }

        public ICollection<Picture> GetPicturesByEmployee(int employeeId)
        {
            return _dataContext.Pictures.Where(p => p.Employee.EmployeeId == employeeId).ToList();
        }

        public bool PictureExists(int pictureId)
        {
            return _dataContext.Pictures.Where(p => p.PictureId == pictureId).Any();
        }

        public bool Save()
        {
            var saved = _dataContext.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
