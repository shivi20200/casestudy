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
    public class JobSeekerService : IJobSeekerService
    {
        private readonly ApplicationDbContext _context;

        public JobSeekerService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> CreateJobSeekerAsync(CreateJobSeekerDto createJobSeekerDto)
        {
            // Check if User exists
            var user = await _context.Users.FindAsync(createJobSeekerDto.UserId);
            if (user == null)
                throw new ArgumentException("User not found.");

            // Check if Job Seeker already exists for this User
            if (_context.JobSeekers.Any(js => js.UserId == createJobSeekerDto.UserId))
                throw new ArgumentException("Job Seeker profile already exists for this user.");

            // Create Job Seeker
            var jobSeeker = new JobSeeker
            {
                UserId = createJobSeekerDto.UserId,
                ProfileSummary = createJobSeekerDto.ProfileSummary,
                Experience = createJobSeekerDto.Experience,
                Skills = createJobSeekerDto.Skills
            };

            _context.JobSeekers.Add(jobSeeker);
            await _context.SaveChangesAsync();

            return "Job Seeker profile created successfully.";
        }


        public async Task<JobSeekerDetailsDto> GetJobSeekerByIdAsync(int seekerId)
        {
            var jobSeeker = await _context.JobSeekers
                .Include(js => js.User) // Include related User details
                .FirstOrDefaultAsync(js => js.SeekerId == seekerId);

            if (jobSeeker == null)
                throw new ArgumentException("Job Seeker not found.");

            return new JobSeekerDetailsDto
            {
                SeekerId = jobSeeker.SeekerId,
                UserId = jobSeeker.UserId,
                ProfileSummary = jobSeeker.ProfileSummary,
                Experience = jobSeeker.Experience,
                Skills = jobSeeker.Skills,
                UserName = jobSeeker.User.Name,
                UserEmail = jobSeeker.User.Email
            };
        }
    }
}
