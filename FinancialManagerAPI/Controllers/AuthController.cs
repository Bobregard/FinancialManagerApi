using AutoMapper;
using FinancialManager.Core.DTOs;
using FinancialManager.Interfaces;
using FinancialManager.Models;
using FinancialManager.Models.DTOs;
using FinancialManager.Service.Interfaces;
using FinancialManager.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinancialManager.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthController : BaseApiController
    {
        public AuthController(IUnitOfWork unitOfWork, ILoggerManager logger, IMapper mapper) : base(unitOfWork, logger, mapper)
        {
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegistrationDto userRegistration)
        {
            var userResult = await _unitOfWork.UserAuthentication.RegisterUserAsync(userRegistration);
            return !userResult.Succeeded ? new BadRequestObjectResult(userResult) : StatusCode(201);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Authenticate([FromBody] UserLoginDto user)
        {
            return !await _unitOfWork.UserAuthentication.ValidateUserAsync(user)
                ? Unauthorized()
                : Ok(new { Token = await _unitOfWork.UserAuthentication.CreateTokenAsync() });
        }

    }
}
