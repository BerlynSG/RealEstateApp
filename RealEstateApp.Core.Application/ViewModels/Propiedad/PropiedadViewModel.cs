using RealEstateApp.Core.Application.ViewModels.Agente;
using RealEstateApp.Core.Application.ViewModels.Mejora;
using RealEstateApp.Core.Application.ViewModels.TipoPropiedad;
using RealEstateApp.Core.Application.ViewModels.TipoVenta;

namespace RealEstateApp.Core.Application.ViewModels.Propiedad
{
    public class PropiedadViewModel
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public double Valor { get; set; }
        public int Habitaciones { get; set; }
        public int Baños { get; set; }
        public double Tamaño { get; set; }
        public string Descripcion { get; set; }
        public bool Favorito { get; set; }

        public TipoPropiedadViewModel TipoPropiedad { get; set; }
        public TipoVentaViewModel TipoVenta { get; set; }
        public List<string> Imagenes { get; set; }
        public List<MejoraViewModel> Mejoras { get; set; }
        public AgenteViewModel Agente { get; set; }
    }
}
