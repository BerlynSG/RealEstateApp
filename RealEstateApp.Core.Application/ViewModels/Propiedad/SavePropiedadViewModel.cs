using Microsoft.AspNetCore.Http;
using RealEstateApp.Core.Application.ViewModels.Mejora;
using RealEstateApp.Core.Application.ViewModels.TipoPropiedad;
using RealEstateApp.Core.Application.ViewModels.TipoVenta;
using System.ComponentModel.DataAnnotations;

namespace RealEstateApp.Core.Application.ViewModels.Propiedad
{
    public class SavePropiedadViewModel
    {
        public string? Codigo { get; set; }

        [Required(ErrorMessage = "Se requiere tener asignado un Tipo de Propiedad.")]
        public int TipoPropiedadId { get; set; }

        public List<string>? Imagenes { get; set; }

        [Required(ErrorMessage = "Se requiere tener asignado un Tipo de Venta.")]
        public int TipoVentaId { get; set; }

        [Required(ErrorMessage = "Se requiere tener asignado Precio.")]
        public double Valor { get; set; }

        [Required(ErrorMessage = "Se requiere tener asignado una Candidad de Habitaciones.")]
        public int Habitaciones { get; set; }

        [Required(ErrorMessage = "Se requiere tener asignado una Cantidad de Baños.")]
        public int Baños { get; set; }

        [Required(ErrorMessage = "Se requiere tener asignado un Tamaño.")]
        public double Tamaño { get; set; }

        [Required(ErrorMessage = "Se requiere tener asignado una Descripción.")]
        public string Descripcion { get; set; }

        public int AgenteId { get; set; }

        //[Required(ErrorMessage = "Se requiere tener asignado al menos una Mejora.")]
        public string? Mejoras { get; set; }

        public IList<IFormFile>? ImagenesFiles { get; set; }

        public List<TipoPropiedadViewModel>? ListaTipoPropiedad { get; set; }
        public List<TipoVentaViewModel>? ListaTipoVenta { get; set; }
        public List<MejoraViewModel>? ListaMejora { get; set; }
    }
}
