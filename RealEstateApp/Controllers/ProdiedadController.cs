using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Enums;
using RealEstateApp.Core.Application.ViewModels.Propiedad;
using RealEstateApp.Models;
using System.Diagnostics;

namespace RealEstateApp.Controllers
{
    public class PropiedadController : Controller
    {
        private List<PropiedadViewModel> propiedades;
        public PropiedadController()
        {
            propiedades = new List<PropiedadViewModel>()
            {
                new()
                {
                    Codigo = "153843",
                    Tipo = (int)TipoPropiedad.Apartamento,
                    Imagen = "/img/Propiedades/Apartamento.jpg",
                    TipoVenta = (int)TipoVenta.Alquiler,
                    Valor = 59.99,
                    Baños = 1,
                    Habitaciones = 2,
                    Tamaño = 50
                },
                new()
                {
                    Codigo = "157832",
                    Tipo = (int)TipoPropiedad.Casa,
                    Imagen = "/img/Propiedades/Apartamento.jpg",
                    TipoVenta = (int)TipoVenta.Alquiler,
                    Valor = 129.99,
                    Baños = 3,
                    Habitaciones = 4,
                    Tamaño = 100
                },
                new()
                {
                    Codigo = "953782",
                    Tipo = (int)TipoPropiedad.Apartamento,
                    Imagen = "/img/Propiedades/Apartamento.jpg",
                    TipoVenta = (int)TipoVenta.Venta,
                    Valor = 33.99,
                    Baños = 1,
                    Habitaciones = 1,
                    Tamaño = 45
                },
                new()
                {
                    Codigo = "775262",
                    Tipo = (int)TipoPropiedad.Casa,
                    Imagen = "/img/Propiedades/Apartamento.jpg",
                    TipoVenta = (int)TipoVenta.Venta,
                    Valor = 89.99,
                    Baños = 2,
                    Habitaciones = 2,
                    Tamaño = 60
                },
            };
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
