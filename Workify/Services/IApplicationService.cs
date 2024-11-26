using Workify.DTOs;
using Workify.Models;
namespace Workify.Services
{
    public interface IApplicationService
    {
        Task<Application> SubmitApplicationAsync(ApplicationDto applicationDto);
        Task<ApplicationMinimalDto> GetMinimalApplicationDetailsAsync(int applicationId);
    }
}
