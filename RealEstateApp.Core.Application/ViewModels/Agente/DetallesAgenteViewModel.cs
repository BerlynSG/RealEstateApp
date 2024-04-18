using RealEstateApp.Core.Application.ViewModels.Propiedad;
using RealEstateApp.Core.Application.ViewModels.TipoPropiedad;

namespace RealEstateApp.Core.Application.ViewModels.Agente
{
    public class DetallesAgenteViewModel
    {
        public AgenteViewModel Agente { get; set; }
        public FiltroPropiedadViewModel Filtros { get; set; }
        public List<TipoPropiedadViewModel> TiposPropiedad { get; set; }
    }
}
