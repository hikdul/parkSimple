using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using parking.DataTransferObjects;
using parking.Models;
using Parking.helper;

namespace parking.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly string _url = "https://localhost:44306/API/parkingAPI";

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<vehiculo> lista = HttpSolicitudes.GetList<vehiculo>(_url + "/todo");


            return View( new ReporteDiario()
            {
                totalVehiculos = lista.Count(),
                VehiculosQueNoHanSalido = lista.FindAll(pre => pre.fechaO == null).Count(),
                VehiculosIngresadosHoy = lista.FindAll(pre => pre.fechaI == DateTime.Now.ToString("dd/MM/yyyy")).Count(),
                VehiculosQueHanSalido = lista.FindAll(pre => pre.fechaO == DateTime.Now.ToString("dd/MM/yyyy")).Count(),

            });




            //return View();
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
