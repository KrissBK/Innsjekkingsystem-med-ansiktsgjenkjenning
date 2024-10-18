using FaceRecogApp.Dto;
using FaceRecogApp.Models;

namespace FaceRecogApp.Interfaces
{
    public interface IEmployeeRepository
    {
        ICollection<Employee> GetEmployees();

        Employee GetEmployee(int id);

        public Employee GetEmployeeByGuid(Guid guid);

        public IList<Employee> GetAllEmployeesByTime();

        bool EmployeeExists(int employeeId);

        bool CreateEmployee(Employee employee);

        bool DeleteEmployee(Employee employee);

        bool UpdateEmployee(Employee employee);

        public bool UpdateEmployeeAttendance(EmployeeAttendanceUpdateDto updateDto);

        bool Save();

    }
}
