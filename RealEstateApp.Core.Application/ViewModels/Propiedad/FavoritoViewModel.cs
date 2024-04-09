namespace RealEstateApp.Core.Application.ViewModels.Propiedad
{
    public class FavoritoViewModel
    {
        public int Id { get; set; }
        public int PropiedadId { get; set; }
        public string UserId { get; set; }

        public PropiedadViewModel Propiedad { get; set; }
    }
}
