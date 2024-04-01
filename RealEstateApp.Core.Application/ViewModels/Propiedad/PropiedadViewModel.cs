using RealEstateApp.Core.Application.ViewModels.Agente;
using RealEstateApp.Core.Application.ViewModels.Mejora;

namespace RealEstateApp.Core.Application.ViewModels.Propiedad
{
    public class PropiedadViewModel
    {
        public string Codigo { get; set; }
        public int Tipo { get; set; }
        public List<string> Imagenes { get; set; }
        public int TipoVenta { get; set; }
        public double Valor { get; set; }
        public int Habitaciones { get; set; }
        public int Baños { get; set; }
        public double Tamaño { get; set; }
        public string Descripcion { get; set; }
        public AgenteViewModel Agente { get; set; }
        public List<MejoraViewModel> Mejoras { get; set; }
    }
}
