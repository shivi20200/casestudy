using Workify.DTOs;

namespace Workify.Services

{
    public interface IJobSeekerService
    {

        Task<string> CreateJobSeekerAsync(CreateJobSeekerDto createJobSeekerDto);
        Task<JobSeekerDetailsDto> GetJobSeekerByIdAsync(int seekerId);
       

    }
}
