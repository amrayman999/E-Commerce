using E_Commerce.Domain.Entities.Identity;
using E_Commerce.Domain.Exceptions.BadRequest;
using E_Commerce.Domain.Exceptions.NotFound;
using E_Commerce.Domain.Exceptions.UnAuthorized;
using E_Commerce.Service.Abstraction.Auth;
using E_Commerce.Shared.Dtos.Auth;
using Microsoft.AspNetCore.Identity;

namespace E_Commerce.Service.Auth
{
    public class AuthService(UserManager<AppUser> _userManager) : IAuthService
    {
        public async Task<UserResponse?> LoginAsync(LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user is null) throw new UserNotFoundException(request.Email);

            var flag = await _userManager.CheckPasswordAsync(user, request.Password);

            if (!flag) throw new UnAuthorizedException();
            return new UserResponse()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = "TODO"

            };
        }

        public async Task<UserResponse?> RegisterAsync(RegisterRequest request)
        {
            var user = new AppUser()
            {
                DisplayName = request.DisplayName,
                Email = request.Email,
                UserName = request.UserName,
                PhoneNumber = request.PhoneNumber
            };
            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded) throw new RegisterationBadRequestException(result.Errors.Select(E => E.Description).ToList());
            return new UserResponse()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = "TODO"
            };
        }
    }
}
