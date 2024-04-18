using Microsoft.AspNetCore.Http;
using RealEstateApp.Core.Application.ViewModels.Propiedad;

namespace RealEstateApp.Core.Application.ViewModels.Agente
{
    public class AgenteViewModel
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Foto { get; set; }
        public IFormFile? ProfileImage { get; set; }
        public List<PropiedadViewModel> Propiedades { get; set; }
        public string Correo { get; set; }
        public string Celular { get; set; }
        public string Cedula { get; set; }
        public string UserName { get; set; }
        public bool EmailConfirmed { get; set; }
    }
}