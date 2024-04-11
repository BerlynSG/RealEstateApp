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
        public int TipoPropiedadId { get; set; }
        public List<string> Imagenes { get; set; }

        [Required(ErrorMessage = "Se requiere el nombre personal del usuario.")]
        [DataType(DataType.Text)]
        public int TipoVentaId { get; set; }
        public double Valor { get; set; }
        public int Habitaciones { get; set; }
        public int Baños { get; set; }
        public double Tamaño { get; set; }
        public string Descripcion { get; set; }
        public int AgenteId { get; set; }
        //public List<MejoraViewModel> Mejoras { get; set; } // Esto hay que arreglarlo
        public string Mejoras { get; set; }
        //Posible solución: un input de texto oculto donde se agrega o se elimina las opciones en forma de texto dividido por comas y una lista de checkbox que hace la anterior función

        public List<TipoPropiedadViewModel>? ListaTipoPropiedad { get; set; }
        public List<TipoVentaViewModel>? ListaTipoVenta { get; set; }
        public List<MejoraViewModel>? ListaMejora { get; set; }
    }
}
