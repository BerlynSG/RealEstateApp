using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace RealEstateApp.Core.Application.ViewModels.User
{
    public class SaveAdminsViewModel
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "Debe colocar el nombre")]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Debe colocar el apellido")]
        [DataType(DataType.Text)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Debe colocar una cedula")]
        [DataType(DataType.Text)]
        public string? Cedula { get; set; }

        [Required(ErrorMessage = "Debe colocar un correo")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Debe colocar un nombre de usuario")]
        [DataType(DataType.Text)]
        public string Username { get; set; }

        [Required(ErrorMessage = "Debe colocar una contraseña")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "Las contraseñas no coiciden")]
        [Required(ErrorMessage = "Debe de confirmar la contraseña")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        public int Rol { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool HasError { get; set; }
        public string? Error { get; set; }
    }
}