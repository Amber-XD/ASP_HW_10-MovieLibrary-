using System;
using System.Collections.Generic;
using System.Text;
using Bll.DTO;
using Bll.Models.Result;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using AutoMapper;

namespace Bll.Service
{
    public class IdentityAuthService
    {
        private readonly UserManager<User> _userManager;

        private readonly SignInManager<User> _signInManager;

        private readonly JwtTokenService _jwtTokenService;
        private readonly IMapper _mapper;

        public IdentityAuthService(UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper, JwtTokenService jwtTokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<RegistrationResult> RegisterUserAsync(UserDto userDto, string password)
        {
            var configuration = _mapper.ConfigurationProvider;

            try
            {
                _mapper.ConfigurationProvider.AssertConfigurationIsValid();
            }
            catch (AutoMapperConfigurationException ex)
            {
                Console.WriteLine(ex.Message);

                if (ex.Errors != null)
                {
                    System.Diagnostics.Debug.WriteLine(ex.ToString());

                    foreach (var error in ex.Errors)
                    {
                        System.Diagnostics.Debug.WriteLine(error.ToString());
                    }
                }

                throw;
            }

            var user = _mapper.Map<User>(userDto);

            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                return new RegistrationResult
                {
                    Success = true,
                    UserId = user.Id,
                    Errors = Array.Empty<string>()
                };
            }
            else
            {
                return new RegistrationResult
                {
                    Success = false,
                    UserId = null,
                    Errors = result.Errors.Select(e => e.Description).ToArray()
                };
            }

        }

        public async Task<LoginResponseDto?> LoginAsync(LoginDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.Login);

            if (user == null)
            {
                return null;
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, true);
            if (!result.Succeeded)
            {
                return null;
            }

            return _jwtTokenService.CreateToken(user);
        }

        public async Task<bool> ValidateUserCredentialsAsync(LoginDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.Login);
            if (user == null)
            {
                return false;
            }
            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            return result.Succeeded;
        }


    }
}
