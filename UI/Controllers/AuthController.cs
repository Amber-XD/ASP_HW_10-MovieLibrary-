using AutoMapper;
using Bll.DTO;
using Bll.Models.Result;
using Bll.Service;
using Domain.Models;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UI.Contracts;

namespace UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IdentityAuthService _identityAuthService, IValidator<IdentityRegisterRequest> _registervalidator, IValidator<IdentityLoginRequest> _loginValidator, IMapper mapper) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<ActionResult<RegistrationResult>> Register([FromBody] IdentityRegisterRequest data)
        {
            var validationResult = await _registervalidator.ValidateAsync(data);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var user = mapper.Map<UserDto>(data);
            var result = await _identityAuthService.RegisterUserAsync(user, data.Password);
            return Ok(result);
        }



        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseDto>> Login(
            [FromBody] IdentityLoginRequest data)
        {
            var validationResult = await _loginValidator.ValidateAsync(data);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var loginDto = mapper.Map<LoginDto>(data);

            var loginResponse = await _identityAuthService.LoginAsync(loginDto);

            if (loginResponse == null)
            {
                return Unauthorized("Invalid login or password.");
            }

            return Ok(loginResponse);
        }

        [Authorize]
        [HttpGet("test-auth")]
        public IActionResult TestAuth()
        {
            return Ok(new
            {
                Message = "JWT authorization works!",
                UserName = User.Identity?.Name
            });
        }

    }
}