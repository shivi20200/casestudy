using Workify.Models;
using Workify.DTOs;
namespace Workify.Services

{
    public interface IUserService
    {
        Task<string> RegisterUserAsync(RegisterDto registerDto);
        Task<string> AuthenticateUserAsync(LoginDto loginDto);
    }
}
