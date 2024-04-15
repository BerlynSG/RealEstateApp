using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using RealEstateApp.Core.Application.Dtos.Account;
using RealEstateApp.Core.Application.Enums;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Infrastructure.Identity.Entities;
using System.Text;

namespace RealEstateApp.Infrastructure.Identity.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailService _emailService;

        public AccountService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IEmailService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
        }

        public async Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request)
        {
            AuthenticationResponse response = new();

            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                response.HasError = true;
                response.Error = $"Ninguna cuenta registrada con el correo {request.Email}";
                return response;
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = $"Credenciales invalidos para {request.Email}";
                return response;
            }
            if (!user.EmailConfirmed)
            {
                response.HasError = true;
                response.Error = $"Cuenta no activada para {request.Email}";
                return response;
            }

            response.Id = user.Id;
            response.Email = user.Email;
            response.UserName = user.UserName;
            response.Rol = user.Rol;

            var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);

            response.Roles = rolesList.ToList();

            return response;
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<RegisterResponse> RegisterClienteUserAsync(RegisterRequest request, string origin, IFormFile profileImage)
        {
            RegisterResponse response = new RegisterResponse();

            var userWithSameUserName = await _userManager.FindByNameAsync(request.UserName);
            if (userWithSameUserName != null)
            {
                response.HasError = true;
                response.Error = $"Nombre de usuario '{request.UserName}' ya está en uso.";
                return response;
            }

            var userWithSameEmail = await _userManager.FindByEmailAsync(request.Email);
            if (userWithSameEmail != null)
            {
                response.HasError = true;
                response.Error = $"Correo electrónico '{request.Email}' ya está registrado.";
                return response;
            }

            var user = new ApplicationUser
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhoneNumber = request.Phone,
                UserName = request.UserName,
                Rol = request.Rol
            };

            if (profileImage != null)
            {
                // Guardar la imagen en el servidor
                string imagePath = await SaveProfileImageAsync(profileImage);
                user.ImagePath = imagePath;  // Asignar la ruta de la imagen al usuario
            }

            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                //Agregar usuario cliente
                await _userManager.AddToRoleAsync(user, Roles.Cliente.ToString());
                var verificationUri = await SendVerificationEmailUri(user, origin);
                await _emailService.SendAsync(new Core.Application.Dtos.Email.EmailRequest()
                {
                    To = user.Email,
                    Body = $"Por favor confirme su cuenta visitando la siguiente URL {verificationUri}",
                    Subject = "Confirmar registro."
                });
            }
            else
            {
                response.HasError = true;
                response.Error = "Ha ocurrido un error al registrar al usuario.";
            }

            return response;
        }      

        public async Task<RegisterResponse> RegisterAgenteUserAsync(RegisterRequest request, string origin, IFormFile profileImage)
        {
            RegisterResponse response = new RegisterResponse { HasError = false };

            var userWithSameUserName = await _userManager.FindByNameAsync(request.UserName);
            if (userWithSameUserName != null)
            {
                response.HasError = true;
                response.Error = $"Nombre de usuario '{request.UserName}' ya está tomado";
                return response;
            }

            var userWithSameEmail = await _userManager.FindByEmailAsync(request.Email);
            if (userWithSameEmail != null)
            {
                response.HasError = true;
                response.Error = $"Email '{request.Email}' ya está registrado";
                return response;
            }

            var user = new ApplicationUser
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhoneNumber = request.Phone,
                UserName = request.UserName,
                Rol = request.Rol,
            };

            if (profileImage != null)
            {
                // Guardar la imagen en el servidor
                string imagePath = await SaveProfileImageAsync(profileImage);
                user.ImagePath = imagePath;  // Asignar la ruta de la imagen al usuario
            }

            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                // Agregar rol de agente
                await _userManager.AddToRoleAsync(user, Roles.Agente.ToString());
            }
            else
            {
                response.HasError = true;
                response.Error = $"Ha ocurrido un error registrando al usuario.";
                return response;
            }

            return response;
        }

        public async Task<RegisterAdminsResponse> RegisterAdminUserAsync(RegisterAdminsRequest request, string origin)
        {
            RegisterAdminsResponse response = new RegisterAdminsResponse() ;

            var userWithSameUserName = await _userManager.FindByNameAsync(request.UserName);
            if (userWithSameUserName != null)
            {
                response.HasError = true;
                response.Error = $"Nombre de usuario '{request.UserName}' ya está tomado";
                return response;
            }

            var userWithSameEmail = await _userManager.FindByEmailAsync(request.Email);
            if (userWithSameEmail != null)
            {
                response.HasError = true;
                response.Error = $"Email '{request.Email}' ya está registrado";
                return response;
            }
            
            var user = new ApplicationUser
            {                
                FirstName = request.FirstName,
                LastName = request.LastName,
                Cedula = request.Cedula,
                Email = request.Email,                
                UserName = request.UserName,
                Rol = request.Rol = 3,
                EmailConfirmed = request.EmailConfirmed
            };

            _userManager.Options.SignIn.RequireConfirmedEmail = false;

            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                // Agregar rol de adminstrador
                await _userManager.AddToRoleAsync(user, Roles.Administrador.ToString());
            }
            else
            {
                response.HasError = true;
                response.Error = $"Ha ocurrido un error registrando al usuario.";
                return response;
            }

            return response;
        }

        public async Task<RegisterAdminsResponse> RegisterDesarrolladorUserAsync(RegisterAdminsRequest request, string origin)
        {
            RegisterAdminsResponse response = new RegisterAdminsResponse ();

            var userWithSameUserName = await _userManager.FindByNameAsync(request.UserName);
            if (userWithSameUserName != null)
            {
                response.HasError = true;
                response.Error = $"Nombre de usuario '{request.UserName}' ya está tomado";
                return response;
            }

            var userWithSameEmail = await _userManager.FindByEmailAsync(request.Email);
            if (userWithSameEmail != null)
            {
                response.HasError = true;
                response.Error = $"Email '{request.Email}' ya está registrado";
                return response;
            }

            var user = new ApplicationUser
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Cedula = request.Cedula,
                Email = request.Email,
                UserName = request.UserName,
                Rol = request.Rol = 4,
                EmailConfirmed = request.EmailConfirmed
            };

            _userManager.Options.SignIn.RequireConfirmedEmail = false;

            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                // Agregar rol de Desarrollador
                await _userManager.AddToRoleAsync(user, Roles.Desarrollador.ToString());
            }
            else
            {
                response.HasError = true;
                response.Error = $"Ha ocurrido un error registrando al usuario.";
                return response;
            }

            return response;
        }

        private async Task<string> SaveProfileImageAsync(IFormFile imageFile)
        {
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "Agentes");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }

            return Path.Combine("img", "Agentes", uniqueFileName).Replace("\\", "/");
        }

        public async Task<string> ConfirmAccountAsync(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return $"Ninguna cuenta registrada con ese correo";
            }

            token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return $"Cuenta confirmada para {user.Email}. Ahora puedes usar la aplicación";
            }
            else
            {
                return $"Ocurrió un error al confirmar {user.Email}.";
            }
        }

        public async Task<UpdateResponse> UpdateUserAsync(UpdateRequest request, string id)
        {
            UpdateResponse response = new();
            response.HasError = false;
            ApplicationUser user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                if (user.Rol == (int)Roles.Agente)
                {
                    user.FirstName = request.FirstName;
                    user.LastName = request.LastName;
                    user.PhoneNumber = request.PhoneNumber;                    
                    user.ImagePath = request.ImagePath;

                    var userUpdated = await _userManager.UpdateAsync(user);
                    if (!userUpdated.Succeeded)
                    {
                        response.HasError = true;
                        response.Error = "Error intentando actualizar al usuario";
                        return response;

                    }
                    return response;
                }
                else if (user.Rol == (int)Roles.Administrador)
                {
                    user.FirstName = request.FirstName;
                    user.LastName = request.LastName;
                    user.Cedula = request.Cedula;
                    user.UserName = request.UserName;
                    user.Email = request.Email;
                    user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, request.Password);

                    var userUpdated = await _userManager.UpdateAsync(user);
                    if (!userUpdated.Succeeded)
                    {
                        response.HasError = true;
                        response.Error = "Error intentando actualizar al usuario";
                        return response;

                    }
                    return response;
                }
            }
            else
            {
                response.HasError = true;
                response.Error = $"No existen cuentas con el id {id}";
                return response;
            }
            return response;
        }

        private async Task<string> SendVerificationEmailUri(ApplicationUser user, string origin)
        {
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var route = "User/ConfirmEmail";
            var Uri = new Uri(string.Concat($"{origin}/", route));
            var verificationUri = QueryHelpers.AddQueryString(Uri.ToString(), "userId", user.Id);
            verificationUri = QueryHelpers.AddQueryString(verificationUri, "token", code);

            return verificationUri;
        }
    }
}
