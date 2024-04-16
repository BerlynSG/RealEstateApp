using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace RealEstateApp.Core.Application.ViewModels.User
{
    public class SaveUserViewModel
    {
        [Required(ErrorMessage = "Debe colocar el nombre")]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Debe colocar el apellido")]
        [DataType(DataType.Text)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Debe colocar un nombre de usuario")]
        [DataType(DataType.Text)]
        public string Username { get; set; }

        [Required(ErrorMessage = "Debe colocar una contraseña")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "Las contraseñas no coiciden")]
        [Required(ErrorMessage = "Debe colocar una contraseña")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Debe colocar un correo")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.Text)]
        public string Phone { get; set; }

        public int Rol { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile? ProfileImage { get; set; }
        public string? ImagePath { get; set; }
        public bool? EmailConfirmed { get; set; }        
        public bool HasError { get; set; }
        public string? Error { get; set; }
        public int Id { get; set; }
    }
}
