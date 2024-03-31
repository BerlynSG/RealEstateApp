﻿using Microsoft.AspNetCore.Mvc;
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
                }
            };
        }

        public IActionResult Index()
        {
            ListaPropiedadViewModel vm= new ListaPropiedadViewModel();
            vm.propiedades = propiedades.ToList();

            return View(vm);
        }

        [HttpPost]
        public IActionResult Index(ListaPropiedadViewModel vm)
        {
            if (vm != null)
            {
                vm.propiedades = propiedades.Where(p => (vm.TipoPropiedad == p.Tipo || vm.TipoPropiedad == 0)
                && (vm.Habitaciones == p.Habitaciones || vm.Habitaciones == 0) && (vm.Baños == p.Baños || vm.Baños == 0)
                && (vm.PrecioMinimo <= p.Valor || vm.PrecioMinimo == 0) && (vm.PrecioMaximo >= p.Valor || vm.PrecioMaximo == 0)
                && (vm.Codigo == null || vm.Codigo == "" || p.Codigo.Contains(vm.Codigo))).ToList();
                return View(vm);
            }
            return RedirectToRoute(new { controller = "Propiedad", action = "Index" });
        }

        public IActionResult Detalles(string codigo)
        {
            PropiedadViewModel vm = propiedades.FirstOrDefault(p => p.Codigo == codigo);
            return View(vm);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
