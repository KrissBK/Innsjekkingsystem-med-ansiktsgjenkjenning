using FaceRecogApp.Models;

namespace FaceRecogApp.Interfaces
{
    public interface IPictureRepository
    {
        ICollection<Picture> GetPicturesByEmployee(int employeeId);
        bool CreatePicture(Picture picture);
        bool DeletePictue(Picture picture);
        bool PictureExists(int pictureID);
        public bool Save();
    }
}
