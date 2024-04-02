using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Enums;
using RealEstateApp.Core.Application.ViewModels.Agente;
using RealEstateApp.Core.Application.ViewModels.Propiedad;
using System.Collections.Generic;
using System.Linq;

namespace RealEstateApp.Controllers
{
    public class AgenteController : Controller
    {
        private static List<AgenteViewModel> _agentes = new List<AgenteViewModel>
        {
            new AgenteViewModel { Id = 1, Nombre = "Juan", Apellidos = "Perez", Foto = "juan.jpg", Propiedades = new List<PropiedadViewModel>
                {
                    new PropiedadViewModel { Codigo = "001", Tipo = (int)TipoPropiedad.Casa, TipoVenta = (int)TipoVenta.Venta, Valor = 150000, Habitaciones = 3, Baños = 2, Tamaño = 200, Descripcion = "Bonita casa en zona residencial", Imagenes = new List<string> { "casa1.jpg", "casa2.jpg", "casa3.jpg" } },
                    new PropiedadViewModel { Codigo = "002", Tipo = (int)TipoPropiedad.Apartamento, TipoVenta = (int)TipoVenta.Alquiler, Valor = 1000, Habitaciones = 2, Baños = 1, Tamaño = 100, Descripcion = "Acogedor apartamento amueblado", Imagenes = new List<string> { "apto1.jpg", "apto2.jpg" } }
                }
            },
            new AgenteViewModel { Id = 2, Nombre = "Maria", Apellidos = "Gonzalez", Foto = "maria.jpg", Propiedades = new List<PropiedadViewModel>
                {
                    new PropiedadViewModel { Codigo = "003", Tipo = (int)TipoPropiedad.Casa, TipoVenta = (int)TipoVenta.Venta, Valor = 200000, Habitaciones = 4, Baños = 3, Tamaño = 250, Descripcion = "Amplia casa con jardín", Imagenes = new List<string> { "casa4.jpg", "casa5.jpg", "casa6.jpg" } }
                }
            },
            new AgenteViewModel { Id = 3, Nombre = "Carlos", Apellidos = "Lopez", Foto = "carlos.jpg", Propiedades = new List<PropiedadViewModel>
                {
                    new PropiedadViewModel { Codigo = "004", Tipo = (int)TipoPropiedad.Apartamento, TipoVenta = (int)TipoVenta.Venta, Valor = 80000, Habitaciones = 1, Baños = 1, Tamaño = 80, Descripcion = "Moderno apartamento con vista al mar", Imagenes = new List<string> { "apto3.jpg", "apto4.jpg" } },
                    new PropiedadViewModel { Codigo = "005", Tipo = (int)TipoPropiedad.Terreno, TipoVenta = (int)TipoVenta.Venta, Valor = 50000, Tamaño = 500, Descripcion = "Terreno en urbanización cerrada", Imagenes = new List<string> { "terreno1.jpg" } }
                }
            }
        };

        public IActionResult Index(string searchString)
        {
            var agentes = _agentes;

            if (!string.IsNullOrEmpty(searchString))
            {
                agentes = agentes.Where(a => a.Nombre.Contains(searchString) || a.Apellidos.Contains(searchString)).ToList();
            }

            var viewModel = new ListaAgenteViewModel
            {
                Agentes = agentes.OrderBy(a => a.Nombre).ToList(),
                SearchString = searchString
            };

            return View(viewModel);
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
