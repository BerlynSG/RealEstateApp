using RealEstateApp.Core.Application.Dtos.Account;
using RealEstateApp.Core.Application.ViewModels.User;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using RealEstateApp.Core.Application.ViewModels.Agente;

namespace RealEstateApp.Core.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<AuthenticationResponse> LoginAsync(LoginViewModel vm);
        Task<string> ConfirmEmailAsync(string userId, string token);
        Task<RegisterResponse> RegisterClienteAsync(SaveUserViewModel vm, string origin, IFormFile profileImage);
        Task<RegisterResponse> RegisterAgenteAsync(SaveUserViewModel vm, string origin, IFormFile profileImage);
        Task<RegisterAdminsResponse> RegisterAdminAsync(SaveAdminsViewModel vm, string origin);
        Task<RegisterAdminsResponse> RegisterDesarrolladorAsync(SaveAdminsViewModel vm, string origin);
        Task<SaveUserViewModel> GetUserById(string id);
        Task SignOutAsync();
        Task<UpdateResponse> UpdateUserAsync(AgenteViewModel vm, string id);
        Task<UpdateResponse> UpdateUserAsync(SaveUserViewModel vm, string id);
    }
}
