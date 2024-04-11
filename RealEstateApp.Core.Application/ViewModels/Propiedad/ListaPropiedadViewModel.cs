﻿using RealEstateApp.Core.Application.ViewModels.TipoPropiedad;

namespace RealEstateApp.Core.Application.ViewModels.Propiedad
{
    public class ListaPropiedadViewModel
    {
        public List<PropiedadViewModel> propiedades { get; set; }
        public List<TipoPropiedadViewModel> tiposPropiedad { get; set; }
        public FiltroPropiedadViewModel Filtros { get; set; }

        //de preuba
        public bool Cliente { get; set; }
        public bool Mantenimiento { get; set; }
    }
}
