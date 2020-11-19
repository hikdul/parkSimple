using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using parking.Models;
using Parking.helper;

namespace parking.Controllers
{
    public class CostoController : Controller
    {

        private readonly string _url = "https://localhost:44306/API/CostoAPI"; 

        /// <summary>
        /// vista inicial donde se ven los detalles del costo habilitado
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            Costo elemento = new Costo();
            elemento = HttpSolicitudes.getById<Costo>(_url, 1);
            return View(elemento);
        }

        public IActionResult Editar(Costo costo)
        {
           
            //var resp = HttpSolicitudes.PostHTTP<Costo>(_url, costo);
            var resp = HttpSolicitudes.PuttHTTP<Costo>(_url,1, costo);
            if (resp)
                return RedirectToAction("Index");
            return View("Index",costo);
        }
    }
}
