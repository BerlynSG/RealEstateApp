﻿namespace RealEstateApp.Core.Application.ViewModels.Propiedad
{
    public class ListaPropiedadViewModel
    {
        public List<PropiedadViewModel> propiedades { get; set; }
        public int TipoPropiedad { get; set; }
        public double PrecioMinimo { get; set; }
        public double PrecioMaximo { get; set; }
        public int Habitaciones { get; set; }
        public int Baños { get; set; }
    }
}
