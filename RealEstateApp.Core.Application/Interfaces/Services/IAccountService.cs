using Microsoft.AspNetCore.Http;
using RealEstateApp.Core.Application.Dtos.Account;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.Interfaces.Services
{
    public interface IAccountService
    {
        Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request);
        Task<string> ConfirmAccountAsync(string userId, string token);
        Task<RegisterResponse> RegisterClienteUserAsync(RegisterRequest request, string origin, IFormFile profileImage);
        Task<RegisterResponse> RegisterAgenteUserAsync(RegisterRequest request, string origin, IFormFile profileImage);
        Task<RegisterResponse> RegisterAdminUserAsync(RegisterRequest request, string origin);
        Task<RegisterResponse> RegisterDesarrolladorUserAsync(RegisterRequest request, string origin);
        Task<UpdateResponse> UpdateUserAsync(UpdateRequest request, string id);

        Task SignOutAsync();
    }
}