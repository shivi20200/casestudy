using Workify.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
using System.Security.Claims;
using System.Text;
using Workify.DTOs;
using Workify.Data;


namespace Workify.Services
{

    public class JobListingService : IJobListingService
    {
        private readonly ApplicationDbContext _context;

        public JobListingService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateJobListingAsync(JobListingDto jobListingDto)
        {
            // Validate employer existence
            var employer = await _context.Employers.FindAsync(jobListingDto.EmployerId);
            if (employer == null)
            {
                throw new ArgumentException("Employer not found.");
            }

            // Create and save the job listing
            var jobListing = new JobListing
            {
                Title = jobListingDto.Title,
                Description = jobListingDto.Description,
                Qualifications = jobListingDto.Qualifications,
                Salary = jobListingDto.Salary,
                JobType = jobListingDto.JobType,
                ReqSkills = jobListingDto.ReqSkills,
                EmployerId = jobListingDto.EmployerId,
                Location = jobListingDto.Location,
                Industry = jobListingDto.Industry
            };

            _context.JobListings.Add(jobListing);
            await _context.SaveChangesAsync();

            return jobListing.JobId;
        }

        // Method to get filtered job listings
        public async Task<IEnumerable<JobListingDto>> GetJobListingsAsync(string jobType = null, string location = null, string industry = null)
        {
            var query = _context.JobListings.AsQueryable();

            // Apply filters if provided
            if (!string.IsNullOrEmpty(jobType))
                query = query.Where(j => j.JobType.Contains(jobType));

            if (!string.IsNullOrEmpty(location))
                query = query.Where(j => j.Location.Contains(location));

            if (!string.IsNullOrEmpty(industry))
                query = query.Where(j => j.Industry.Contains(industry));

            // Select only the necessary fields and map to JobListingDto
            var jobListings = await query.Select(j => new JobListingDto
            {
                JobId = j.JobId,
                Title = j.Title,
                Description = j.Description,
                Qualifications = j.Qualifications,
                Salary = j.Salary,
                JobType = j.JobType,
                ReqSkills = j.ReqSkills,
                EmployerId = j.EmployerId,
                Location = j.Location,
                Industry = j.Industry
            }).ToListAsync();

            return jobListings;
        }

        // Fetch a single job listing by jobId
        public async Task<JobListingDto> GetJobListingByIdAsync(int jobId)
        {
            var jobListing = await _context.JobListings
                .Where(j => j.JobId == jobId)
                .Select(j => new JobListingDto
                {
                    JobId = j.JobId,
                    Title = j.Title,
                    Description = j.Description,
                    Qualifications = j.Qualifications,
                    Salary = j.Salary,
                    JobType = j.JobType,
                    ReqSkills = j.ReqSkills,
                    EmployerId = j.EmployerId,
                    Location = j.Location,
                    Industry = j.Industry
                })
                .FirstOrDefaultAsync();

            return jobListing;
        }
    }
}
