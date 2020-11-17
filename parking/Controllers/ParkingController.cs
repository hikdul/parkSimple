using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using parking.Models;
using Parking.helper;
using QRCoder;

namespace parking.Controllers
{
    public class ParkingController : Controller
    {

        private readonly string _url = "https://localhost:44306/API/parkingAPI";

        /// <summary>
        /// vistra index o inicial donde ira el listado de elementos que estan activos
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// vista donde estara el btn que enviara una señal para generar el codigoQR
        /// </summary>
        /// <returns></returns>
        public IActionResult BtnGenerarQr()
        {
            return View();
        }

        /// <summary>
        /// aqui se generaran los datos y se dedvolvera un QR a la vista para que luego se imprima al cliente
        /// tambien cubrira el proceso de generar los datos en base de datos
        /// todos los procesos por lo general se aran mediente llamadas HTtP
        /// </summary>
        /// 

        public void QRaVista()
        {
            ViewBag.QRCode = DevolverQR();
        }



        /// <summary>
        /// con los datos actuales retorno un QR
        /// </summary>
        /// <returns></returns>
        public Byte[] DevolverQR()
        {
            vehiculo nuevo = new vehiculo()
            {
                costo = 1,
                fechaI = DateTime.Now.ToString("dd/MM/yyyy"),
                horaI = DateTime.Now.ToString("t")

            };

            nuevo = HttpSolicitudes.PostandGetHTTP<vehiculo>(_url, nuevo);

            if (nuevo.id < 1)
                return null;


            return GenerarQR(nuevo.id.ToString() + "=>" + nuevo.fechaI + "=>" + nuevo.horaI);


        }
        /// <summary>
        /// para generar un codigo QR y devolverlo como un array de bytes
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        public Byte[] GenerarQR(string texto)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(texto, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20);

            return BitmapToBytes(qrCodeImage);
        }
        /// <summary>
        /// para generar un array de bites para que los exploradores lo entiendan
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        private static Byte[] BitmapToBytes(Bitmap img)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.ToArray();
            }
        }

    }
}
