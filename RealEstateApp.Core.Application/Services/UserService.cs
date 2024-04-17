using AutoMapper;
using Microsoft.AspNetCore.Http;
using RealEstateApp.Core.Application.Dtos.Account;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.Agente;
using RealEstateApp.Core.Application.ViewModels.User;


namespace RealEstateApp.Core.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPropiedadRepository _propiedadRepository;

        public UserService(IAccountService accountService, IMapper mapper, IHttpContextAccessor httpContextAccessor, IPropiedadRepository propiedadRepository)
        {
            _accountService = accountService;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _propiedadRepository = propiedadRepository;
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

        public async Task<RegisterAdminsResponse> RegisterAdminAsync(SaveAdminsViewModel vm, string origin)
        {
            RegisterAdminsRequest request = _mapper.Map<RegisterAdminsRequest>(vm);
            return await _accountService.RegisterAdminUserAsync(request, origin);
            
        }

        public async Task<RegisterAdminsResponse> RegisterDesarrolladorAsync(SaveAdminsViewModel vm, string origin)
        {
            RegisterAdminsRequest request = _mapper.Map<RegisterAdminsRequest>(vm);
            return await _accountService.RegisterDesarrolladorUserAsync(request, origin);
        }

        private async Task<string> SaveProfileImageAsync(IFormFile imageFile)
        {
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "Agentes");
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

            return Path.Combine("img", "Agentes", uniqueFileName).Replace("\\", "/");
        }
        public async Task<UpdateResponse> UpdateAgentAsync(AgenteViewModel vm, string id)
        {
            UpdateRequest req = _mapper.Map<UpdateRequest>(vm);
            return await _accountService.UpdateUserAsync(req, id);
        }
           

        public async Task<string> ConfirmEmailAsync(string userId, string token)
        {
            return await _accountService.ConfirmAccountAsync(userId, token);
        }
        public async Task<List<AuthenticationResponse>> GetAllUsers()
        {
            List<AuthenticationResponse> users = await _accountService.GetAllUsers();
            return users;
        }
        public async Task<List<SaveUserViewModel>> GetAllViewModel()
        {
            var users = await this.GetAllUsers();
            var usersVm = _mapper.Map<List<SaveUserViewModel>>(users);

            return usersVm;
        }

        public async Task<SaveUserViewModel> GetUserById(string id)
        {
            AuthenticationResponse user = await _accountService.GetUserById(id);
            SaveUserViewModel userMap = _mapper.Map<SaveUserViewModel>(user);
            return userMap;
        }
        public async Task<SaveAdminsViewModel> GetUserByAdminId(string id)
        {
            AuthenticationResponse user = await _accountService.GetUserByAdminId(id);
            SaveAdminsViewModel userMap = _mapper.Map<SaveAdminsViewModel>(user);
            return userMap;
        }

        public async Task<UpdateResponse> ActivateUserAsync(string id)
        {
            return await _accountService.ActivateUserAsync(id);
        }
          public async Task<Dictionary<string, int>> GetAdminDashboardData()
        {
            Dictionary<string, int> dashboardData = new Dictionary<string, int>();

            // Obtener la cantidad de propiedades registradas
            int totalPropertiesCount = await _propiedadRepository.GetTotalPropertiesCountAsync();
            dashboardData.Add("TotalPropertiesCount", totalPropertiesCount);

            // Obtener la cantidad de agentes activos e inactivos
            int activeAgentsCount = await _accountService.GetActiveAgentsCount();
            int inactiveAgentsCount = await _accountService.GetInactiveAgentsCount();
            dashboardData.Add("ActiveAgentsCount", activeAgentsCount);
            dashboardData.Add("InactiveAgentsCount", inactiveAgentsCount);

            // Obtener la cantidad de clientes activos e inactivos
            int activeClientsCount = await _accountService.GetActiveClientsCount();
            int inactiveClientsCount = await _accountService.GetInactiveClientsCount();
            dashboardData.Add("ActiveClientsCount", activeClientsCount);
            dashboardData.Add("InactiveClientsCount", inactiveClientsCount);

            // Obtener la cantidad de desarrolladores activos e inactivos
            int activeDevelopersCount = await _accountService.GetActiveDevelopersCount();
            int inactiveDevelopersCount = await _accountService.GetInactiveDevelopersCount();
            dashboardData.Add("ActiveDevelopersCount", activeDevelopersCount);
            dashboardData.Add("InactiveDevelopersCount", inactiveDevelopersCount);

            return dashboardData;
        }
        public async Task<UpdateResponse> DeleteUserAsync(string id)
        {
            return await _accountService.DeleteUserAsync(id);
        }

    }
}
