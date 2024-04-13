namespace RealEstateApp.Core.Application.ViewModels.Mejora
{
    public class ListaMejoraViewModel
    {
        public List<MejoraViewModel> Mejoras { get; set; }
        public int IdDelete { get; set; }

        public int MessageType { get; set; }
        public string Message { get; set; }
    }
}
