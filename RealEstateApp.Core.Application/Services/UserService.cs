using AutoMapper;
using Microsoft.AspNetCore.Http;
using RealEstateApp.Core.Application.Dtos.Account;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.User;
using System.IO;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(IAccountService accountService, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _accountService = accountService;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<AuthenticationResponse> LoginAsync(LoginViewModel vm)
        {
            AuthenticationRequest loginRequest = _mapper.Map<AuthenticationRequest>(vm);
            return await _accountService.AuthenticateAsync(loginRequest);
        }

        public async Task SignOutAsync()
        {
            await _accountService.SignOutAsync();
        }

        public async Task<RegisterResponse> RegisterClienteAsync(SaveUserViewModel vm, string origin, IFormFile profileImage)
        {
            RegisterRequest registerRequest = _mapper.Map<RegisterRequest>(vm);

            if (profileImage != null)
            {
                string imagePath = await SaveProfileImageAsync(profileImage);
                registerRequest.ImagePath = imagePath;
            }

            return await _accountService.RegisterClienteUserAsync(registerRequest, origin, profileImage);
        }

        public async Task<RegisterResponse> RegisterAgenteAsync(SaveUserViewModel vm, string origin, IFormFile profileImage)
        {
            RegisterRequest registerRequest = _mapper.Map<RegisterRequest>(vm);

            if (profileImage != null)
            {
                string imagePath = await SaveProfileImageAsync(profileImage);
                registerRequest.ImagePath = imagePath;
            }

            return await _accountService.RegisterAgenteUserAsync(registerRequest, origin, profileImage);
        }

        private async Task<string> SaveProfileImageAsync(IFormFile imageFile)
        {
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "profile");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var uniqueFileName = Path.GetRandomFileName() + "_" + imageFile.FileName;
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }

            return Path.Combine("images", "profile", uniqueFileName).Replace("\\", "/");
        }

        public async Task<string> ConfirmEmailAsync(string userId, string token)
        {
            return await _accountService.ConfirmAccountAsync(userId, token);
        }
    }
}
