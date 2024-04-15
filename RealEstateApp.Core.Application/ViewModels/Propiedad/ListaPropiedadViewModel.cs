using RealEstateApp.Core.Application.ViewModels.TipoPropiedad;

namespace RealEstateApp.Core.Application.ViewModels.Propiedad
{
    public class ListaPropiedadViewModel
    {
        public List<PropiedadViewModel> propiedades { get; set; }
        public List<TipoPropiedadViewModel> tiposPropiedad { get; set; }
        public FiltroPropiedadViewModel Filtros { get; set; }

        public int Modo { get; set; }

        public int EliminarId { get; set; }

        public int MessageType { get; set; }
        public string Message { get; set; }
    }
}
