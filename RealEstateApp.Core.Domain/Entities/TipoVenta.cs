﻿namespace RealEstateApp.Core.Domain.Entities
{
    public class TipoVenta
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public List<Propiedad> Propiedades { get; set; }
    }
}
