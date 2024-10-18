using FaceRecogApp.Web.Interfaces;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;

namespace FaceRecogApp.Web.Services
{
    public class FaceService : IFaceService
    {
        private readonly HttpClient _httpClient;
        public FaceService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IList<Guid>> IdentifyEmployee(string imageString)
        {

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(imageString), 
                                                            Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/FaceService/Recognition?PersonGroupId=test1", stringContent);

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return new List<Guid>();
                }
                     
                return await response.Content.ReadFromJsonAsync<IList<Guid>>();
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                throw new Exception(message);
            }
        }

        public async Task<bool> AddFaceToEmployee(string personGroupId, int employeeId, string imageString)
        {
            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(imageString),
                                                           Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"api/FaceService/Face?personGroupId={personGroupId}&employeeId={employeeId}", stringContent);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> TrainPersonGroup(string personGroupId)
        {
            var response = await _httpClient.GetAsync($"api/FaceService/train/{personGroupId}");

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

