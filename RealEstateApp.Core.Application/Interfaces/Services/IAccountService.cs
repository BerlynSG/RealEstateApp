using Microsoft.AspNetCore.Http;
using RealEstateApp.Core.Application.Dtos.Account;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.Interfaces.Services
{
    public interface IAccountService
    {
        Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request);
        Task SignOutAsync();
        Task<RegisterResponse> RegisterClienteUserAsync(RegisterRequest request, string origin, IFormFile profileImage);
        Task<RegisterResponse> RegisterAgenteUserAsync(RegisterRequest request, string origin, IFormFile profileImage);
        Task<RegisterAdminsResponse> RegisterAdminUserAsync(RegisterAdminsRequest request, string origin);
        Task<RegisterAdminsResponse> RegisterDesarrolladorUserAsync(RegisterAdminsRequest request, string origin);
        Task<UpdateResponse> UpdateUserAsync(UpdateRequest request, string id);
        Task<string> ConfirmAccountAsync(string userId, string token);
        Task<AuthenticationResponse> GetUserById(string id);
        Task<AuthenticationResponse> GetUserByAdminId(string id);
        Task<UpdateResponse> ActivateUserAsync(string id);
        Task<List<AuthenticationResponse>> GetAllUsers();
        Task<int> GetActiveAgentsCount();
        Task<int> GetInactiveAgentsCount();
        Task<int> GetActiveClientsCount();
        Task<int> GetInactiveClientsCount();
        Task<int> GetActiveDevelopersCount();
        Task<int> GetInactiveDevelopersCount();
        Task<UpdateResponse> DeleteUserAsync(string id);
    }
}