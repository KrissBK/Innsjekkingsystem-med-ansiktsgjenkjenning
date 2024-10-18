using Microsoft.Azure.CognitiveServices.Vision.Face.Models;

namespace FaceRecogApp.Interfaces
{
    public interface IFaceServiceRepository
    {
        Task<List<DetectedFace>> DetectFaceInImage(byte[] imageArray, string recognitionModel);
        Task<List<Guid>> IdentifyDetectedFace(List<DetectedFace> detectedFaces, string personGroupId);
        Task<List<Guid>> DetectAndIdentifyFaceInImage(string personGroupId, byte[] imageArray, string recognitionModel);
        Task CreatePersonGroup(string personGroupId, string personGroupName);
        Task<Person> AddPersonToPersonGroup(string personGroupId, string employeeNumber);
        Task<PersistedFace> AddFaceToPerson(string personGroupId, Guid personId, byte[] imageArray);
        Task<bool> TrainPersonGroup(string personGroupId);
        Task DeletePersonGroup(string personGroupId);
        Task DeletePerson(string personGroupId, Guid personId);
        Task<ICollection<PersonGroup>> GetAllPersonGroups();
        Task<PersonGroup?> GetPersonGroup(string personGroupId);
        Task<ICollection<Person>> GetAllPersons(string personGroupId);
    }
}
