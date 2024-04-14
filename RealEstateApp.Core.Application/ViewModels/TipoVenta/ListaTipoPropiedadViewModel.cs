namespace RealEstateApp.Core.Application.ViewModels.TipoVenta
{
    public class ListaTipoVentaViewModel
    {
        public List<TipoVentaViewModel> TiposVenta { get; set; }
        public int IdDelete { get; set; }

        public int MessageType { get; set; }
        public string Message { get; set; }
    }
}
