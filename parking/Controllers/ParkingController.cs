using System;
using System.Collections.Generic;
using ms =System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using iText.Barcodes;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using Microsoft.AspNetCore.Mvc;
using parking.Models;
using Parking.helper;
using QRCoder;

namespace parking.Controllers
{
    public class ParkingController : Controller
    {
        /// <summary>
        /// url de la unbicacion de mi api rest
        /// </summary>
        private readonly string _url = "https://localhost:44306/API/parkingAPI";
        /// <summary>
        /// par extraer el valor de mi costo desde database
        /// </summary>
        private readonly string _urlCosto = "https://localhost:44306/API/CostoApi";


        

        // =========================== ### ===========================
        // mis vistas
        // =========================== ### ===========================


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

        // =========================== ### ===========================
        // componentes de comunicacion
        // =========================== ### ===========================

        /// <summary>
        /// con los datos actuales retorno un QR
        /// este Qr se realiza con los nuevos datos del vehiculo
        /// </summary>
        /// <returns></returns>
        public FileResult DevolverQR()
        {

            vehiculo nuevo = new vehiculo()
            {
                id = 1,
                costo = 1,
                fechaI = DateTime.Now.ToString("dd/MM/yyyy"),
                horaI = DateTime.Now.ToString("t")

            };

            nuevo = HttpSolicitudes.PostandGetHTTP<vehiculo>(_url+ "/insertarDatos", nuevo);

            return QRenPDF(nuevo);

        }
        
        /// <summary>
        /// para generar un pdf con el qr de la informacion necesaria del nuevo ingreso vehicular
        /// </summary>
        /// <param name="nuevo"></param>
        /// <returns></returns>
         public FileResult QRenPDF(vehiculo nuevo)
        {

            using (MemoryStream ms = new MemoryStream())
            {
                PdfWriter writer = new PdfWriter(ms);
                using(var pdfDoc = new PdfDocument(writer)){
                    Document doc = new Document(pdfDoc,new PageSize(150,200));
                    doc.SetMargins(10, 10, 0, 10);
                    //agrego datos
                    Paragraph c1 = new Paragraph("Ingreso Vehicular");
                    Paragraph c2 = new Paragraph("Hora Ingreso: " + nuevo.horaI);
                    Paragraph c3 = new Paragraph("Fecha Ingreso: " + nuevo.fechaI);
                    BarcodeQRCode brQR = new BarcodeQRCode(nuevo.id.ToString() + "=>" + nuevo.horaI +"=>"+nuevo.horaI);
                    Image imgQR = new Image(brQR.CreateFormXObject(pdfDoc));
                    //agrego estilos
                    c1.SetFontSize(10);
                    c1.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
                    c2.SetFontSize(5);
                    c2.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
                    c3.SetFontSize(5);
                    c3.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
                    imgQR.ScaleToFit(120, 120);
                    imgQR.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
                    //agrego al documento
                    doc.Add(c1);
                    doc.Add(c2);
                    doc.Add(c3);
                    doc.Add(imgQR);
                    //cierro para enviar
                    doc.Close();
                    writer.Close();
                }
                return File(ms.ToArray(), "application/pdf");
            }

        }


        /// <summary>
        /// aqui retorno los datos del con lo obtenido por el qr
        /// </summary>
        /// <param name="textoQr"></param>

        public vehiculo DevolverSalida(string textoQr)
        {
            //formato del texto
            //nuevo.id.ToString() + "=>" + nuevo.horaI +"=>"+nuevo.horaI


            string[] datos = textoQr.Split("=>");

            vehiculo salida = new vehiculo();
            salida.id = Int32.Parse(datos[0].Trim());
            salida.fechaI = datos[1].Trim();
            salida.horaI = datos[2].Trim();
            salida.fechaO = DateTime.Now.ToString("dd/MM/yyyy");
            salida.horaO = DateTime.Now.ToString("t");

            salida = HttpSolicitudes.PutAndGetHTTP<vehiculo>(_url+ "/salida", Int32.Parse(datos[0].Trim()), salida);
            salida.tiempo = CalcularCosto(salida);

            return salida;

        }
        /// <summary>
        /// para calcular el valor total a pagar en base a mi costo
        ///  8:00 pm > tiempo nocturno > 8:00 am
        ///  tambien verifica las fracciones de tiempo: 
        ///  tiempo < 30 min
        ///  tiempo < 15 min
        ///  tiempo < 5 min
        /// </summary>
        /// <param name="vehiculo"></param>
        /// <returns></returns>
        private string CalcularCosto(vehiculo vehiculo)
        {

            double valorACobrar = 0;
            Costo costo = HttpSolicitudes.getById<Costo>(_urlCosto, 1);

            var fi = DateTime.Parse(vehiculo.fechaI + " " + vehiculo.horaI);
            var fo = DateTime.Parse(vehiculo.fechaO + " " + vehiculo.horaO);

            var Htotales = (fo - fi).ToString(@"dd\d\ hh\h\ mm\m\ ");
            string[] valores = Htotales.Split(" ");

            //calculo minutos

            int min = Int32.Parse( valores[2].Substring(0, 2));

            if (min < 59 && min > 30)
            { 
                valorACobrar += costo.hora;
            }
            else if(min > 15 && min <=30)
            {
                valorACobrar += costo.f30;
            }
            else if(min > 5 && min <=15)
            {
                valorACobrar += costo.f15;
            }
            else if(min <= 5)
            {
                valorACobrar += costo.f5;
            }

            //calcular horas
            int hours = Int32.Parse(valores[1].Substring(0, 2));

            //calcular dias
            int days = Int32.Parse(valores[0].Substring(0, 2));
            //"tiempo": "01d 05h 07m "
            return Htotales;
        }
        /// <summary>
        /// tiempo en horas nocturnas
        /// </summary>
        /// <param name="vehiculo"></param>
        /// <returns></returns>
        private int tiempoNocturno(vehiculo vehiculo)
        {

            string[] fechaI = vehiculo.fechaI.Split("/");
            string[] fechaO = vehiculo.fechaO.Split("/");
            string[] horaI = vehiculo.horaI.Split(":");
            string[] horaO = vehiculo.horaO.Split(":");


            int yI = Int32.Parse(fechaI[0]);
            int mI = Int32.Parse(fechaI[1]);
            int dI = Int32.Parse(fechaI[2]);
            int hI=  Int32.Parse(horaI[0]) ;
            int mI=  Int32.Parse(horaI[1]) ;
            int yO = Int32.Parse(fechaO[0]);
            int mO = Int32.Parse(fechaO[1]);
            int dO = Int32.Parse(fechaO[2]);
            int hO = Int32.Parse(horaO[0]);
            int mO = Int32.Parse(horaO[1]);





            var fi = DateTime.Parse(vehiculo.fechaI + " " + vehiculo.horaI);
            var fo = DateTime.Parse(vehiculo.fechaO + " " + vehiculo.horaO);





            var Htotales = (fo - fi).ToString(@"dd\d\ hh\h\ mm\m\ ");
            string[] valores = Htotales.Split(" ");

            int min = Int32.Parse(valores[2].Substring(0, 2));

            //calcular horas
            int hours = Int32.Parse(valores[1].Substring(0, 2));

            //calcular dias
            int days = Int32.Parse(valores[0].Substring(0, 2));

            if(days == 0)
            {

            }

            return 0;
        }

    }
}
