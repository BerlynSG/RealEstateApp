namespace RealEstateApp.Core.Domain.Entities
{
    public class Propiedad
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public int TipoPropiedadId { get; set; }
        public int TipoVentaId { get; set; }
        public double Valor { get; set; }
        public int Habitaciones { get; set; }
        public int Baños { get; set; }
        public double Tamaño { get; set; }
        public string Descripcion { get; set; }
        public string AgenteId { get; set; }

        public TipoPropiedad TipoPropiedad { get; set; }
        public TipoVenta TipoVenta { get; set; }
        public List<ImagenPropiedad> Imagenes { get; set; }
        public List<MejoraPropiedad> Mejoras { get; set; }
        public List<PropiedadFavorita> Favoritos { get; set; }
    }
}
