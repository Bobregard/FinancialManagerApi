using FinancialManager.Core.DTOs;
using FinancialManager.Models;
using FinancialManager.Models.DTOs;
using Microsoft.AspNetCore.Identity;

namespace FinancialManager.Interfaces
{
    public interface IUserAuthenticationRepository
    {
        Task<IdentityResult> RegisterUserAsync(UserRegistrationDto userForRegistration);
        Task<bool> ValidateUserAsync(UserLoginDto loginDto);
        Task<string> CreateTokenAsync();
    }
}
