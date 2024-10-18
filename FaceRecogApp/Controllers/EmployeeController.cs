using AutoMapper;
using FaceRecogApp.Dto;
using FaceRecogApp.Interfaces;
using FaceRecogApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace FaceRecogApp.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]

    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IJobTitleRepository _jobTitleRepository;
        private readonly IFaceServiceRepository _faceServiceRepository;

        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeRepository employeeRepository, 
            IJobTitleRepository jobTitleRepository,
            IFaceServiceRepository faceServiceRepository,
            IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _jobTitleRepository = jobTitleRepository;
            _faceServiceRepository = faceServiceRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<EmployeeDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetEmployees() 
        {
            var employees = _mapper.Map<List<EmployeeDto>>(_employeeRepository.GetEmployees());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(employees);
        }

        [HttpGet("ByTime")]
        [ProducesResponseType(200, Type = typeof(IList<EmployeeDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetEmployeesByTime()
        {
            var employees = _mapper.Map<List<EmployeeDto>>(_employeeRepository.GetAllEmployeesByTime());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(employees);
        }


        [HttpGet("{EmployeeId}")]
        [ProducesResponseType(200, Type = typeof(EmployeeDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetEmployee(int employeeId)
        {
            var employee = _mapper.
                Map<EmployeeDto>(_employeeRepository.
                GetEmployee(employeeId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_employeeRepository.EmployeeExists(employeeId))
            {
                return NotFound();
            }

            return Ok(employee);
        }


        [HttpGet("guid/{faceServicePersonId}")]
        [ProducesResponseType(200, Type = typeof(EmployeeDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetEmployeeByGuid(Guid faceServicePersonId)
        {
            var employee = _mapper.
                Map<EmployeeDto>(_employeeRepository.
                GetEmployeeByGuid(faceServicePersonId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_employeeRepository.EmployeeExists(employee.EmployeeId))
            {
                return NotFound();
            }

            return Ok(employee);
        }
        

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]
        public IActionResult CreateEmployee([FromQuery] int jobTitleId, [FromQuery] string personGroupId, [FromBody] EmployeeDto employeeCreate)
        {
            if (employeeCreate == null)
            {
                return BadRequest(ModelState);
            }
            
            var employee = _employeeRepository.GetEmployees()
                .Where(e => e.EmployeeId == employeeCreate.EmployeeId)
                .FirstOrDefault();

            if (employee != null) {
                ModelState.AddModelError("", "Employee already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var employeeMap = _mapper.Map<Employee> (employeeCreate);

            employeeMap.JobTitle = _jobTitleRepository.GetJobTitle(jobTitleId);
            employeeMap.faceServicePersonId = _faceServiceRepository.AddPersonToPersonGroup(personGroupId, employeeCreate.FirstName).Result.PersonId;

            if (!_employeeRepository.CreateEmployee(employeeMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving employee");
                return StatusCode(500, ModelState);
            }

            
            return Ok("Successfully created employee");
        }


        [HttpPatch]
        public IActionResult UpdateEmployeeAttendance([FromBody] EmployeeAttendanceUpdateDto employeeUpdateDto)
        {
            try
            {

                if (employeeUpdateDto == null)
                {
                    return BadRequest(ModelState);
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (!_employeeRepository.EmployeeExists(employeeUpdateDto.EmployeeId))
                {
                    return NotFound();
                }

                if (!_employeeRepository.UpdateEmployeeAttendance(employeeUpdateDto))
                {
                    ModelState.AddModelError("", "Something went wrong while trying to update");
                    return StatusCode(500, ModelState);
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


         [HttpDelete("{EmployeeId}/{personGroupId}")]
         [ProducesResponseType(204)]
         [ProducesResponseType(400)]
         [ProducesResponseType(404)]
         public IActionResult DeleteEmployee(int employeeId, string personGroupId)
         {
            if (_employeeRepository.EmployeeExists == null)
            {
                return NotFound(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var employeeToDelete = _employeeRepository.GetEmployee(employeeId);
            if (!_employeeRepository.DeleteEmployee(employeeToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while trying to delete");
            }

            _faceServiceRepository.DeletePerson(personGroupId, employeeToDelete.faceServicePersonId);

            return NoContent();

        }
    }
}
