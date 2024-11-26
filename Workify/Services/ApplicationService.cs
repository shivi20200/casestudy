using Workify.Models;
using Workify.DTOs;
using Microsoft.EntityFrameworkCore;
using Workify.Data;

namespace Workify.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly ApplicationDbContext _context;

        public ApplicationService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Application> SubmitApplicationAsync(ApplicationDto applicationDto)
        {
            // Check if job exists
            var job = await _context.JobListings.FindAsync(applicationDto.JobId);
            if (job == null)
            {
                throw new ArgumentException("Job listing not found.");
            }

            // Check if job seeker exists
            var jobSeeker = await _context.JobSeekers.FindAsync(applicationDto.SeekerId);
            if (jobSeeker == null)
            {
                throw new ArgumentException("Job seeker not found.");
            }

            // Create new Application
            var application = new Application
            {
                SeekerId = applicationDto.SeekerId,
                JobId = applicationDto.JobId,
                Resume = applicationDto.Resume,
                Status = applicationDto.Status,
                AppliedOn = DateTime.UtcNow
            };

            // Add application to the database
            _context.Applications.Add(application);
            await _context.SaveChangesAsync();

            return application;
        }


        public async Task<ApplicationMinimalDto> GetMinimalApplicationDetailsAsync(int applicationId)
        {
            // Fetch application with related JobSeeker and JobListing
            var application = await _context.Applications
                .Include(a => a.JobListing)
                .Include(a => a.JobSeeker)
                .ThenInclude(js => js.User) // Assuming the User entity has the name
                .FirstOrDefaultAsync(a => a.ApplicationId == applicationId);

            if (application == null)
            {
                throw new ArgumentException("Application not found.");
            }

            // Map to the minimal DTO
            return new ApplicationMinimalDto
            {
                SeekerName = application.JobSeeker.User.Name, // Assuming User has a Name property
                JobTitle = application.JobListing.Title,
                Resume = Convert.ToBase64String(application.Resume), // Convert binary resume to Base64
                Status = application.Status,
                AppliedOn = application.AppliedOn
            };
        }

    }
}
