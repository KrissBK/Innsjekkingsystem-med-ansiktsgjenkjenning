namespace FaceRecogApp.Web.Interfaces
{
    public interface IFaceService
    {
        public Task<IList<Guid>> IdentifyEmployee(string imageString);

        public Task<bool> AddFaceToEmployee(string personGroupId, int employeeId, string imageString);

        public Task<bool> TrainPersonGroup(string personGroupId);

    }
}
