namespace RealEstateApp.Core.Domain.Entities
{
    public class MejoraPropiedad
    {
        public int Id { get; set; }
        public int MejoraId { get; set; }
        public int PropiedadId { get; set; }

        public Mejora Mejora { get; set; }
        public Propiedad Propiedad { get; set; }
    }
}
