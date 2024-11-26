using Microsoft.AspNetCore.Mvc;
using Workify.DTOs;
using Workify.Services;

namespace Workify.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JobSeekersController : ControllerBase
    {
        private readonly IJobSeekerService _jobSeekerService;

        public JobSeekersController(IJobSeekerService jobSeekerService)
        {
            _jobSeekerService = jobSeekerService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateJobSeeker([FromBody] CreateJobSeekerDto createJobSeekerDto)
        {
            try
            {
                var message = await _jobSeekerService.CreateJobSeekerAsync(createJobSeekerDto);
                return Ok(new { Message = message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpGet("{seekerId}")]
        public async Task<IActionResult> GetJobSeekerDetails(int seekerId)
        {
            try
            {
                var jobSeekerDetails = await _jobSeekerService.GetJobSeekerByIdAsync(seekerId);
                return Ok(jobSeekerDetails);
            }
            catch (ArgumentException ex)
            {
                return NotFound(new { Error = ex.Message });
            }
        }
    }
}
