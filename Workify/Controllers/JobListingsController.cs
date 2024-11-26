using Microsoft.AspNetCore.Mvc;
using Workify.DTOs;
using Workify.Services;
using Microsoft.AspNetCore.Authorization;


namespace Workify.Controllers
{
    [ApiController]
        [Route("api/[controller]")]
        public class JobListingsController : ControllerBase
        {
            private readonly IJobListingService _jobListingService;

            public JobListingsController(IJobListingService jobListingService)
            {
                _jobListingService = jobListingService;
            }

            [HttpPost]
            [Authorize(Roles = "Employer")] // Restrict access to employers only
            public async Task<IActionResult> CreateJobListing([FromBody] JobListingDto jobListingDto)
            {
                try
                {
                    var jobId = await _jobListingService.CreateJobListingAsync(jobListingDto);
                    return Ok(new { JobId = jobId, Message = "Job listing created successfully." });
                }
                catch (ArgumentException ex)
                {
                    return BadRequest(new { Error = ex.Message });
                }
            }



        // GET: /api/joblistings?jobType=fulltime&location=NY&industry=tech
        [HttpGet]
        [Authorize(Roles = "JobSeeker, Employer")]  // Optional authorization based on roles
        public async Task<IActionResult> GetJobListings([FromQuery] string jobType, [FromQuery] string location, [FromQuery] string industry)
        {
            try
            {
                // Fetch filtered job listings using the service
                var jobListings = await _jobListingService.GetJobListingsAsync(jobType, location, industry);

                // Return the filtered job listings in the response
                return Ok(jobListings);
            }
            catch (Exception ex)
            {
                // Return error response if any issue occurs
                return StatusCode(500, new { Error = ex.Message });
            }
        }

        // GET: /api/joblistings/{jobId}
        [HttpGet("{jobId}")]
        [Authorize(Roles = "JobSeeker, Employer")]
        public async Task<IActionResult> GetJobListingById(int jobId)
        {
            try
            {
                var jobListing = await _jobListingService.GetJobListingByIdAsync(jobId);

                if (jobListing == null)
                {
                    return NotFound(new { Error = "Job not found" });
                }

                return Ok(jobListing);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = ex.Message });
            }
        }
    }

    
}
