using FaceRecogApp.Web.Dto;
using FaceRecogApp.Web.Interfaces;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;

namespace FaceRecogApp.Web.Services
{ 
    public class EmployeeService : IEmployeeService
    {

        private readonly HttpClient _httpClient;
        public EmployeeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }



        public async Task<EmployeeDto> GetEmployeeByGuid (Guid faceServicePersonId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/Employee/guid/{faceServicePersonId}");

                if (response.IsSuccessStatusCode) 
                {
                    var employee = await response.Content.ReadFromJsonAsync<EmployeeDto>();
                    
                    return employee;
                    
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception(message);
                }

                
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public async Task<List<EmployeeDto>> GetEmployeesByTime()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/Employee/ByTime");

                if (response.IsSuccessStatusCode)
                {
                    var employees = await response.Content.ReadFromJsonAsync<List<EmployeeDto>>();

                    return employees;

                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception(message);
                }


            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<bool> UpdateEmployeeAttendance(Guid faceServicePersonId)
        {
            var employee = await GetEmployeeByGuid(faceServicePersonId);
            if (employee != null)
            {
                EmployeeAttendanceUpdateDto UpdateDto = new EmployeeAttendanceUpdateDto();

                UpdateDto.EmployeeId = employee.EmployeeId;
                UpdateDto.latestCheckIn = DateTime.Now;




                if (employee.Attendance)
                {

                    UpdateDto.Attendance = false;
                    var jsonRequest = JsonConvert.SerializeObject(UpdateDto);
                    var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json-patch+json");
                    var respons = await _httpClient.PatchAsync("api/Employee", content);
                    
                    if (!respons.IsSuccessStatusCode)
                    {
                        return false;
                    }
                    return true;

                }
                else
                {
                    UpdateDto.Attendance = true;
                    var jsonRequest = JsonConvert.SerializeObject(UpdateDto);
                    var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json-patch+json");
                    var respons = await _httpClient.PatchAsync("api/Employee", content);

                    if (!respons.IsSuccessStatusCode)
                    {
                        return false;
                    }
                    return true;

                }
            }

            return false;

        }

        public async Task<bool> CreateEmployee(EmployeeDto employee, int jobTitleId, string personGroupId)
        {
            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(employee),
                                                            Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"/api/Employee?JobTitleId={jobTitleId}&personGroupId={personGroupId}", stringContent);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<List<EmployeeDto>> GetEmployees()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/Employee");

                if (response.IsSuccessStatusCode)
                {
                    var employees = await response.Content.ReadFromJsonAsync<List<EmployeeDto>>();

                    return employees;

                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception(message);
                }


            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> DeleteEmployee(int employeeId, string personGroupId)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/Employee/{employeeId}/{personGroupId}");

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
