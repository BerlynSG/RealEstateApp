using RealEstateApp.Core.Application.Dtos.Account;
using RealEstateApp.Core.Application.ViewModels.User;

namespace RealEstateApp.Core.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<string> ConfirmEmailAsync(string userId, string token);
        Task<AuthenticationResponse> LoginAsync(LoginViewModel vm);
        Task<RegisterResponse> RegisterAsync(SaveUserViewModel vm, string origin);
        Task SignOutAsync();
        //Task<SaveUserViewModel> GetByIdSaveViewModel(string id);

        Task<SaveUserViewModel> Add(SaveUserViewModel vm);
        Task Update(SaveUserViewModel vm, string? id);
    }
}