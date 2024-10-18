using FaceRecogApp.Interfaces;
using FaceRecogApp.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;

namespace FaceRecogApp.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]

    public class FaceServiceController: Controller
    {
        private readonly IFaceServiceRepository _faceServiceRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public FaceServiceController(IEmployeeRepository employeeRepository)
        {
            _faceServiceRepository = new FaceServiceRepository();
            _employeeRepository = employeeRepository;
        }

        [HttpPost("PersonGroup")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> CreatePersonGroup([FromQuery] string personGroupId, string persongroupName) 
        {
            await _faceServiceRepository.CreatePersonGroup(personGroupId, persongroupName);

            return Ok("Successfully created ");
        }
        
        [HttpPost("Face")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> AddFaceToPerson([FromQuery] string personGroupId, [FromQuery] int employeeId, [FromBody] string imageString)
        {
            var employee = _employeeRepository.GetEmployee(employeeId);

            byte[] imageByteArray = Convert.FromBase64String(imageString);

            var face = await _faceServiceRepository.AddFaceToPerson(personGroupId, employee.faceServicePersonId, imageByteArray);

            if (face != null)
            {
                return Ok();
            }

            return BadRequest(ModelState);
            
        }
         
        [HttpPost("Recognition")]
        [ProducesResponseType(200, Type = typeof(IList<Guid>))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetIdentifiedEmployee([FromBody] string imageString, [FromQuery] string personGroupId)
        {
            byte[] imageByteArray = Convert.FromBase64String(imageString);

            try
            {
                var identifiedPeople = await _faceServiceRepository.DetectAndIdentifyFaceInImage(personGroupId, imageByteArray, RecognitionModel.Recognition04);

                return Ok(identifiedPeople);
            }
            catch (APIErrorException)
            {
                return NoContent();
            }
            catch (Exception)
            {
                return BadRequest();
            }
            
        }

        [HttpGet("Persons")]
        [ProducesResponseType(200, Type = typeof(ICollection<Person>))]
        public async Task<IActionResult> GetAllPersons(string personGroupId)
        {
            var people = await _faceServiceRepository.GetAllPersons(personGroupId);

            return Ok(people);
        }

        [HttpGet("Train/{personGroupId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> TrainPersonGroup(string personGroupId)
        {
            if (!await _faceServiceRepository.TrainPersonGroup(personGroupId))
            {
                return BadRequest();
            }
            
            return Ok();
        }

        [HttpGet("PersonGroups")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<PersonGroup>))]
        public async Task<IActionResult> GetAllPersonGroups()
        {
            var groups = await _faceServiceRepository.GetAllPersonGroups();

            return Ok(groups);
        }

        [HttpGet("{personGroupId}")]
        [ProducesResponseType(200, Type = typeof(PersonGroup))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetPersonGroup(string personGroupId)
        {
            var group = await _faceServiceRepository.GetPersonGroup(personGroupId);
            
            if (group == null)
            {
                return NotFound();
            }

            return Ok(group);
        }

        [HttpDelete("{personGroupId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> DeletePersonGroup(string personGroupId)
        {
            if (_faceServiceRepository.GetPersonGroup == null)
            {
                return BadRequest();
            }

            await _faceServiceRepository.DeletePersonGroup(personGroupId);

            return NoContent();
        }
    }
}
 