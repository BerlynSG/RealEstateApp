﻿using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.ViewModels.Agente;
using RealEstateApp.Core.Application.ViewModels.Propiedad;
using RealEstateApp.Core.Application.ViewModels.TipoPropiedad;
using RealEstateApp.Core.Application.ViewModels.TipoVenta;

namespace RealEstateApp.Controllers
{
    public class AgenteController : Controller
    {
        private List<TipoPropiedadViewModel> tiposPropiedad = new()
        {
            new(), new(){ Nombre = "Apartamento" }, new(){ Nombre = "Casa" }, new(){ Nombre = "Terreno" }
        };
        private List<TipoVentaViewModel> tiposVenta = new()
        {
            new (), new (){ Nombre = "Alquiler" }, new (){ Nombre = "Venta" }
        };
        private static List<AgenteViewModel> _agentes;

        public AgenteController()
        {
            _agentes = new List<AgenteViewModel>
            {
                new AgenteViewModel { Id = 1, Nombre = "Juan", Apellidos = "Perez", Foto = "juan.jpg", Propiedades = new List<PropiedadViewModel>
                    {
                        new PropiedadViewModel { Codigo = "001", TipoPropiedad = tiposPropiedad[2], TipoVenta = tiposVenta[2], Valor = 150000, Habitaciones = 3, Baños = 2, Tamaño = 200, Descripcion = "Bonita casa en zona residencial", Imagenes = new List<string> { "casa1.jpg", "casa2.jpg", "casa3.jpg" } },
                        new PropiedadViewModel { Codigo = "002", TipoPropiedad = tiposPropiedad[1], TipoVenta = tiposVenta[1], Valor = 1000, Habitaciones = 2, Baños = 1, Tamaño = 100, Descripcion = "Acogedor apartamento amueblado", Imagenes = new List<string> { "apto1.jpg", "apto2.jpg" } }
                    }
                },
                new AgenteViewModel { Id = 2, Nombre = "Maria", Apellidos = "Gonzalez", Foto = "maria.jpg", Propiedades = new List<PropiedadViewModel>
                    {
                        new PropiedadViewModel { Codigo = "003", TipoPropiedad = tiposPropiedad[2], TipoVenta = tiposVenta[2], Valor = 200000, Habitaciones = 4, Baños = 3, Tamaño = 250, Descripcion = "Amplia casa con jardín", Imagenes = new List<string> { "casa4.jpg", "casa5.jpg", "casa6.jpg" } }
                    }
                },
                new AgenteViewModel { Id = 3, Nombre = "Carlos", Apellidos = "Lopez", Foto = "carlos.jpg", Propiedades = new List<PropiedadViewModel>
                    {
                        new PropiedadViewModel { Codigo = "004", TipoPropiedad = tiposPropiedad[2], TipoVenta = tiposVenta[2], Valor = 80000, Habitaciones = 1, Baños = 1, Tamaño = 80, Descripcion = "Moderno apartamento con vista al mar", Imagenes = new List<string> { "apto3.jpg", "apto4.jpg" } },
                        new PropiedadViewModel { Codigo = "005", TipoPropiedad = tiposPropiedad[3], TipoVenta = tiposVenta[2], Valor = 50000, Tamaño = 500, Descripcion = "Terreno en urbanización cerrada", Imagenes = new List<string> { "terreno1.jpg" } }
                    }
                }
            };
        }

        public IActionResult Index(string searchTerm)
        {
            List<AgenteViewModel> agentes = _agentes;

            if (!string.IsNullOrEmpty(searchTerm))
            {
                agentes = agentes
                    .Where(a => a.Nombre.Contains(searchTerm, System.StringComparison.OrdinalIgnoreCase) || a.Apellidos.Contains(searchTerm, System.StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            agentes = agentes.OrderBy(a => a.Nombre).ToList();

            return View(new ListaAgenteViewModel { Agentes = agentes, SearchTerm = searchTerm });
        }

        public IActionResult Detalles(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agente = _agentes.FirstOrDefault(a => a.Id == id);
            if (agente == null)
            {
                return NotFound();
            }

            return View(agente);
        }

        public IActionResult Propiedades(int? codigoAgente)
        {
            if (codigoAgente == null)
            {
                return NotFound();
            }

            var agente = _agentes.FirstOrDefault(a => a.Id == codigoAgente);
            if (agente == null)
            {
                return NotFound();
            }

            return View(agente.Propiedades);
        }
    }
}
