using E_Commerce.Service.Abstraction;
using E_Commerce.Shared.Dtos.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Presentation.API.Controllers
{
    public class AuthController(IServiceManager _serviceManager) : APIBaseController
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var result = await _serviceManager.AuthService.LoginAsync(request);
            return Ok(result);
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var result = await _serviceManager.AuthService.RegisterAsync(request);
            return Ok(result);
        }
        [HttpGet("EmailExists")]
        public async Task<IActionResult> CheckEmailExists([FromQuery] string email)
        {
            var result = await _serviceManager.AuthService.CheckEmailExistAsync(email);
            return Ok(result);
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetCurrentUser()
        {
            var email = User.FindFirst(ClaimTypes.Email);
            var result = await _serviceManager.AuthService.GetCurrentUserAsync(email.Value);
            return Ok(result);
        }
        [HttpGet("Address")]
        [Authorize]
        public async Task<IActionResult> GetCurrentUserAddress()
        {
            var email = User.FindFirst(ClaimTypes.Email);
            var result = await _serviceManager.AuthService.GetCurrentUserAddressAsync(email.Value);
            return Ok(result);
        }
        [HttpPut("Address")]
        [Authorize]
        public async Task<IActionResult> UpdateCurrentUserAddress(AddressDto request)
        {
            var email = User.FindFirst(ClaimTypes.Email);
            var result = await _serviceManager.AuthService.UpdateCurrentUserAddressAsync(request, email.Value);
            return Ok(result);
        }
    }
}
