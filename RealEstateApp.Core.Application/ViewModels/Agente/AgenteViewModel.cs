using Microsoft.AspNetCore.Http;
using RealEstateApp.Core.Application.ViewModels.Propiedad;
using RealEstateApp.Core.Application.ViewModels.TipoPropiedad;

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

        //public FiltroPropiedadViewModel Filtros { get; set; }
        //public List<TipoPropiedadViewModel> TiposPropiedad { get; set; }
    }
}