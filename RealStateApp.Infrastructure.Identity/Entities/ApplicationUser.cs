﻿using Microsoft.AspNetCore.Identity;
    
    namespace RealEstateApp.Infrastructure.Identity.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Cedula { get; set; }
        public int Rol { get; set; }
        public string? ImagePath { get; set; }
        public bool IsVerified { get; internal set; }
    }
}
