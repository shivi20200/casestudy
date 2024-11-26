using Microsoft.AspNetCore.Mvc;
using Workify.DTOs;
using Workify.Services;

namespace Workify.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApplicationsController : ControllerBase
    {
        private readonly IApplicationService _applicationService;

        public ApplicationsController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        // POST /api/applications
        [HttpPost]
        public async Task<IActionResult> SubmitApplication([FromBody] ApplicationDto applicationDto)
        {
            if (applicationDto == null)
            {
                return BadRequest("Invalid application data.");
            }

            try
            {
                var application = await _applicationService.SubmitApplicationAsync(applicationDto);
                return Ok(new { Message = "Application submitted successfully", ApplicationId = application.ApplicationId });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpGet("{applicationId}")]
        public async Task<IActionResult> GetApplicationMinimalDetails(int applicationId)
        {
            try
            {
                var applicationDetails = await _applicationService.GetMinimalApplicationDetailsAsync(applicationId);
                return Ok(applicationDetails);
            }
            catch (ArgumentException ex)
            {
                return NotFound(new { Error = ex.Message });
            }
        }

    }
}
