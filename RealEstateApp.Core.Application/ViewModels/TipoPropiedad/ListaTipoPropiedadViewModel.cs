namespace RealEstateApp.Core.Application.ViewModels.TipoPropiedad
{
    public class ListaTipoPropiedadViewModel
    {
        public List<TipoPropiedadViewModel> TiposPropiedad { get; set; }
        public int IdDelete { get; set; }

        public int MessageType { get; set; }
        public string Message { get; set; }
    }
}
