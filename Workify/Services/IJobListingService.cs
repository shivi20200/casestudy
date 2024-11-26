using Workify.DTOs;
namespace Workify.Services
{
    public interface IJobListingService
    {
        Task<int> CreateJobListingAsync(JobListingDto jobListingDto);
        Task<IEnumerable<JobListingDto>> GetJobListingsAsync(string jobType = null, string location = null, string industry = null);
 
        Task<JobListingDto> GetJobListingByIdAsync(int jobId);
    }
}
