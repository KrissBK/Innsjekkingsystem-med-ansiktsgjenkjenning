using FaceRecogApp.Data;
using FaceRecogApp.Dto;
using FaceRecogApp.Interfaces;
using FaceRecogApp.Models;

namespace FaceRecogApp.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DataContext _dataContext;
        public EmployeeRepository(DataContext dataContext) { 
            _dataContext = dataContext;

        }

        public ICollection<Employee> GetEmployees()
        {
            return _dataContext.Employees.OrderBy(e => e.EmployeeId).ToList();
        }

        public Employee GetEmployee(int id)
        {
            return _dataContext.Employees.FirstOrDefault(e => e.EmployeeId == id);
        }

        public Employee GetEmployeeByGuid(Guid guid)
        {
            return _dataContext.Employees.FirstOrDefault(e => e.faceServicePersonId == guid);

        }

        public IList<Employee> GetAllEmployeesByTime()
        {
            return _dataContext.Employees.Where(e=> e.Attendance == true).OrderByDescending(e => e.latestCheckIn).Take(10).ToList();
        }

        public JobTitle GetJobTitleByEmployeeId(int employeeId)
        {
            return _dataContext.Employees.Where(e => e.EmployeeId == employeeId).Select(j => j.JobTitle).FirstOrDefault();
        }
       
        public bool Save()
        {
            var saved = _dataContext.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool EmployeeExists(int employeeId)
        {
            return _dataContext.Employees.Where(e => e.EmployeeId == employeeId).Any();
        }

        public bool CreateEmployee(Employee employee)
        {
            _dataContext.Add(employee);

            return Save();
        }

        public bool DeleteEmployee(Employee employee)
        {
            _dataContext.Remove(employee);

            return Save();
        }

        public bool UpdateEmployee(Employee employee)
        {
            _dataContext.Update(employee);
            return Save();
        }

        public bool UpdateEmployeeAttendance(EmployeeAttendanceUpdateDto updateDto)
        {
            var employee = GetEmployee(updateDto.EmployeeId);

            if (employee != null)
            {
                employee.Attendance = updateDto.Attendance;
                employee.latestCheckIn = updateDto.latestCheckIn;
                return Save();
                
            }

            return false;
        }
    }
}
