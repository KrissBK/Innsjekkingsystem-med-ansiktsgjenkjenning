using FaceRecogApp.Interfaces;
using FaceRecogApp.Models;
using Microsoft.AspNetCore.Mvc;
using FaceRecogApp.Dto;
using AutoMapper;

namespace FaceRecogApp.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]

    public class ActivityController : Controller
    {
        private readonly IActivityRepository _activityRepository;
        private readonly IEmployeeRepository _employeeRepository;

        private readonly IMapper _mapper;
        public ActivityController(IActivityRepository activityRepository,
            IEmployeeRepository employeeRepository,
            IMapper mapper)
        {
            _activityRepository = activityRepository;
            _employeeRepository = employeeRepository;

            _mapper = mapper;

        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ActivityDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetActivities()
        {
            var activities = _mapper.
                Map<List<ActivityDto>>(_activityRepository.
                GetActivities());

            foreach (var activity in activities)
            {
                var organizer = _activityRepository.GetEmployeeByActivityId(activity.ActivityId);
                activity.Organizer = _mapper.Map<EmployeeDto>(organizer);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(activities);
        }

        [HttpGet("{ActivityId}/Organizer")]
        [ProducesResponseType(200, Type = typeof(EmployeeDto))]
        [ProducesResponseType(400)]
        public IActionResult GetOrganizerByActivityId(int activityId)
        {
            var activities = _mapper.
                Map<EmployeeDto>(_activityRepository.
                GetEmployeeByActivityId(activityId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(activities);
        }


        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateActivity([FromQuery] int employeeId, [FromBody] ActivityDto activityCreate)
        {
            if (activityCreate == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var employee = _employeeRepository.GetEmployee(employeeId);
            if (employee == null)
            {
                return NotFound($"Employee with ID {employeeId} not found.");
            }


            var activityMap = _mapper.Map<Activity>(activityCreate);
            activityMap.Organizer = employee;

            if (!_activityRepository.CreateActivity(activityMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving the activity");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully created");
        }

        [HttpPut("{ActivityId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateActivity(int activityId, [FromQuery] int organizerId, [FromBody] ActivityDto activityUpdate)
        {
            if (activityUpdate == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (activityId != activityUpdate.ActivityId)
            {
                return BadRequest(ModelState);
            }

            if (!_activityRepository.ActivityExists(activityId))
            {
                return NotFound();
            }
            var organizer = _employeeRepository.GetEmployee(organizerId);
            var activityMap = _mapper.Map<Activity>(activityUpdate);
            activityMap.Organizer = organizer;

            if (!_activityRepository.UpdateActivity(activityMap))
            {
                ModelState.AddModelError("", "Something went wrong while trying to update");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{ActivityId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteActivity(int activityId)
         {
           

            if (!_activityRepository.ActivityExists(activityId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var activityToDelete = _activityRepository.GetActivity(activityId);

            if (!_activityRepository.DeleteActivity(activityToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while trying to delete");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

    }
}