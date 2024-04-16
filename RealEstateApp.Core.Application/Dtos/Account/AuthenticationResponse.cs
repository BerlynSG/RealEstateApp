﻿
namespace RealEstateApp.Core.Application.Dtos.Account
{
    public class AuthenticationResponse
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string? ImagePath { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Cedula { get; set; }
        public List<string> Roles { get; set; }
        public int Rol { get; set; }

        public bool EmailConfirmed { get; set; }
        public bool HasError { get; set; }
        public string? Error { get; set; }

    }
}
