using FaceRecogApp.Interfaces;
using Microsoft.Azure.CognitiveServices.Vision.Face;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;

namespace FaceRecogApp.Repositories
{
    public class FaceServiceRepository : IFaceServiceRepository
    {
        private readonly IFaceClient _client;
        public FaceServiceRepository()
        {
            _client = new FaceClient(new ApiKeyServiceClientCredentials(Environment.GetEnvironmentVariable("VISION_KEY"))) { Endpoint = Environment.GetEnvironmentVariable("VISION_ENDPOINT") };
        }


        public async Task CreatePersonGroup(string personGroupId, string personGroupName)
        {
            // Bør kanskje finne ut av hvilken responskode vi får fra Face Service
            // Kan da bruke til feilsjekking ved å returnere bool for status på create
            await _client.PersonGroup.CreateAsync(personGroupId, personGroupName, recognitionModel: RecognitionModel.Recognition04);
            Console.WriteLine($"Created PersonGroup: {personGroupName}");
        }

        public async Task<Person> AddPersonToPersonGroup(string personGroupId, string employeeName)
        {
            Person person = await _client.PersonGroupPerson.CreateAsync(personGroupId, employeeName);
            Console.WriteLine($"Created Person: {employeeName}");
            return person;
        }

        public async Task<PersistedFace> AddFaceToPerson(string personGroupId, Guid personId, byte[] imageArray)
        {
            using (MemoryStream stream = new MemoryStream(imageArray))
            {
                PersistedFace face = await _client.PersonGroupPerson.AddFaceFromStreamAsync(personGroupId, personId, stream);

                if (face != null)
                {
                    Console.WriteLine($"Added face to Person: {personId}");
                    return face;
                }
                return default;
            }
        }

        public async Task<bool> TrainPersonGroup(string personGroupId)
        {
            await _client.PersonGroup.TrainAsync(personGroupId);
            Console.WriteLine($"Training PersonGroup: {personGroupId}");
            while (true)
            {
                await Task.Delay(1000);
                var trainingStatus = await _client.PersonGroup.GetTrainingStatusAsync(personGroupId);
                if (trainingStatus.Status == TrainingStatusType.Succeeded)
                {
                    Console.WriteLine($"Training succeeded");
                    return true;
                }
                else if (trainingStatus.Status == TrainingStatusType.Failed)
                {
                    Console.WriteLine($"Training failed");
                    return false;
                }
            }
        }

        public async Task<List<DetectedFace>> DetectFaceInImage(byte[] imageArray, string recognitionModel)
        {
            
            using (MemoryStream stream = new MemoryStream(imageArray))
            {
                IList<DetectedFace> detectedFaces = await _client.Face.DetectWithStreamAsync(stream,
                    recognitionModel: recognitionModel, detectionModel: DetectionModel.Detection03);

                List<DetectedFace> detectedFaceList = new List<DetectedFace>();
                foreach (var detectedFace in detectedFaces)
                {
                    Console.WriteLine($"Detected face {detectedFace.FaceId} in image.");
                    detectedFaceList.Add(detectedFace);
                }

                return detectedFaceList;
            }

        }

        public async Task <List<Guid>> IdentifyDetectedFace(List<DetectedFace> detectedFaces, string personGroupId)
        {
            List<Guid> sourceFaceIds = new List<Guid>();

            foreach (var detectedFace in detectedFaces)
            {
                sourceFaceIds.Add(detectedFace.FaceId.Value);
            }

            var identificationResults = await _client.Face.IdentifyAsync(sourceFaceIds, personGroupId, confidenceThreshold:0.8);

            List<Guid> identifiedPeople = new List<Guid>();

            foreach (var identificationResult in identificationResults)
            {
                if (identificationResult.Candidates.Count == 0)
                {
                    Console.WriteLine($"No person(s) recognized");
                    continue;
                }
                Person person = await _client.PersonGroupPerson.GetAsync(personGroupId, identificationResult.Candidates[0].PersonId);
                Console.WriteLine($"Person '{person.Name}' is identified for - {identificationResult.FaceId}, confidence: {identificationResult.Candidates[0].Confidence}.");

                VerifyResult verifyResult = await _client.Face.VerifyFaceToPersonAsync(identificationResult.FaceId, person.PersonId, personGroupId);
                Console.WriteLine($"Verification result: is a match? {verifyResult.IsIdentical}. Confidence: {verifyResult.Confidence}");
                identifiedPeople.Add(person.PersonId);
            }
            return identifiedPeople;
        }

        public async Task<List<Guid>> DetectAndIdentifyFaceInImage(string personGroupId, byte[] imageArray, string recognitionModel)
        {
            List<DetectedFace> DetectedFaces = await DetectFaceInImage(imageArray, recognitionModel);
            
            return await IdentifyDetectedFace(DetectedFaces, personGroupId);

        }

        public async Task DeletePersonGroup(string personGroupId)
        {
            // lese API-respons for å se om det virket
            await _client.PersonGroup.DeleteAsync(personGroupId);
            Console.WriteLine($"PersonGroup: {personGroupId} deleted");
        }

        public async Task DeletePerson(string personGroupId, Guid personId)
        {
            await _client.PersonGroupPerson.DeleteAsync(personGroupId, personId);
        }

        public async Task<ICollection<PersonGroup>> GetAllPersonGroups()
        {
            var personGroups = await _client.PersonGroup.ListAsync();

            return personGroups;
        }

        public async Task<PersonGroup?> GetPersonGroup(string personGroupId)
        {
            try { 
                var personGroup = await _client.PersonGroup.GetAsync(personGroupId);
                return personGroup;

            } catch (Exception) { 

                Console.WriteLine($"something went wrong");
                return null;
            }
            

            
        }

        public async Task<ICollection<Person>> GetAllPersons(string personGroupId)
        {
            var persons = await _client.PersonGroupPerson.ListAsync(personGroupId);
            
            return persons;
        }
    }
}

