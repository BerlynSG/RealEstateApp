using RealEstateApp.Core.Application.ViewModels.TipoPropiedad;

namespace RealEstateApp.Core.Application.ViewModels.Propiedad
{
    public class ListaPropiedadViewModel
    {
        public List<PropiedadViewModel> propiedades { get; set; }
        public List<TipoPropiedadViewModel> tiposPropiedad { get; set; }
        public int TipoPropiedad { get; set; }
        public double PrecioMinimo { get; set; }
        public double PrecioMaximo { get; set; }
        public int Habitaciones { get; set; }
        public int Baños { get; set; }
        public string Codigo { get; set; }

        //de preuba
        public bool Cliente { get; set; }
        public bool Mantenimiento { get; set; }
    }
}
