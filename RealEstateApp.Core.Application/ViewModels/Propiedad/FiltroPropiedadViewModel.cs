namespace RealEstateApp.Core.Application.ViewModels.Propiedad
{
    public class FiltroPropiedadViewModel
    {
        public int TipoFiltroUsuario { get; set; }
        public string UsuarioId { get; set; }

        public int TipoPropiedad { get; set; }
        public double PrecioMinimo { get; set; }
        public double PrecioMaximo { get; set; }
        public int Habitaciones { get; set; }
        public int Baños { get; set; }
        public string Codigo { get; set; }
    }
}
