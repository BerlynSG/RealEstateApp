namespace RealEstateApp.Core.Domain.Entities
{
    public class ImagenPropiedad
    {
        public int Id { get; set; }
        public int PropiedadId { get; set; }
        public string Path { get; set; }

        public Propiedad Propiedad { get; set; }
    }
}
