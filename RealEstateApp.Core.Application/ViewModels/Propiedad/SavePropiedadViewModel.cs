using RealEstateApp.Core.Application.ViewModels.Agente;
using RealEstateApp.Core.Application.ViewModels.Mejora;
using RealEstateApp.Core.Application.ViewModels.TipoPropiedad;
using RealEstateApp.Core.Application.ViewModels.TipoVenta;
using System.ComponentModel.DataAnnotations;

namespace RealEstateApp.Core.Application.ViewModels.Propiedad
{
    public class SavePropiedadViewModel
    {
        public string Codigo { get; set; }

        [Required(ErrorMessage = "Se requiere tener asignado un tipo de propiedad.")]
        public int TipoPropiedad { get; set; }
        public List<string> Imagenes { get; set; }

        [Required(ErrorMessage = "Se requiere el nombre personal del usuario.")]
        [DataType(DataType.Text)]
        public int TipoVenta { get; set; }
        public double Valor { get; set; }
        public int Habitaciones { get; set; }
        public int Baños { get; set; }
        public double Tamaño { get; set; }
        public string Descripcion { get; set; }
        public AgenteViewModel Agente { get; set; }
        public List<MejoraViewModel> Mejoras { get; set; }

        public List<TipoPropiedadViewModel>? ListaTipoPropiedad { get; set; }
        public List<TipoVentaViewModel>? ListaTipoVenta { get; set; }
        public List<MejoraViewModel>? ListaMejora { get; set; }
    }
}
