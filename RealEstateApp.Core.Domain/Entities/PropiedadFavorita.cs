namespace RealEstateApp.Core.Domain.Entities
{
    public class PropiedadFavorita
    {
        public int Id { get; set; }
        public int PropiedadId { get; set; }
        public string ClienteId { get; set; }

        public Propiedad Propiedad { get; set; }
    }
}
