using AutoMapper;
using FaceRecogApp.Dto;
using FaceRecogApp.Interfaces;
using FaceRecogApp.Models;
using FaceRecogApp.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FaceRecogApp.Controllers
{
    
    [Route("/api/[controller]")]
    [ApiController]
    public class PictureController : Controller
    {
        private readonly IPictureRepository _pictureRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public PictureController(IPictureRepository pictureRepository, 
            IEmployeeRepository employeeRepository,
            IMapper mapper)
        {
            _pictureRepository = pictureRepository;
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        [HttpGet("{EmployeeId}")]
        [ProducesResponseType(200, Type = typeof(ICollection<PictureDto>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetPicturesByEmployee(int employeeId) { 
            var pictures = _mapper.Map<List<PictureDto>>(_pictureRepository.GetPicturesByEmployee(employeeId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_employeeRepository.EmployeeExists(employeeId))
            {
                return NotFound();
            }

            return Ok(pictures);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult CreatePicture([FromQuery] int employeeId, [FromBody] PictureDto pictureCreate)
        {
            if (pictureCreate == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var PictureMap = _mapper.Map<Picture>(pictureCreate);

            PictureMap.Employee = _employeeRepository.GetEmployee(employeeId);

            if (!_pictureRepository.CreatePicture(PictureMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving picure");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created ");
        }
    }
}
