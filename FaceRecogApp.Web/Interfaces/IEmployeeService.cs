using FaceRecogApp.Web.Dto;

namespace FaceRecogApp.Web.Interfaces
{
    public interface IEmployeeService
    {
        public Task<EmployeeDto> GetEmployeeByGuid(Guid faceServicePersonId);

        public Task<bool> UpdateEmployeeAttendance(Guid faceServicePersonId);

        public Task<List<EmployeeDto>> GetEmployeesByTime();

        Task<bool> CreateEmployee(EmployeeDto employee, int jobTitleId, string personGroupId);

        Task<List<EmployeeDto>> GetEmployees();

        Task<bool> DeleteEmployee(int employeeId, string personGroupId);
    }
}
