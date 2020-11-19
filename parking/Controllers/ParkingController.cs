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
using parking.DataTransferObjects;

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

        public IActionResult SalidaVehicular(string texto)
        {
            InfSalidaVehiculo inf = new InfSalidaVehiculo();
            if (String.IsNullOrEmpty(texto))
                inf = null;
            else
                inf = DevolverSalida(texto);

           
            return View(inf);
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

        public InfSalidaVehiculo DevolverSalida(string textoQr)
        {
            //formato del texto
            //nuevo.id.ToString() + "=>" + nuevo.horaI +"=>"+nuevo.horaI

            if (String.IsNullOrEmpty(textoQr))
                return null;

            string[] datos = textoQr.Split("=>");

            vehiculo salida = new vehiculo();
            salida.id = Int32.Parse(datos[0].Trim());
            salida.fechaI = datos[1].Trim();
            salida.horaI = datos[2].Trim();
            salida.fechaO = DateTime.Now.ToString("dd/MM/yyyy");
            salida.horaO = DateTime.Now.ToString("t");

            salida = HttpSolicitudes.PutAndGetHTTP<vehiculo>(_url+ "/salida", Int32.Parse(datos[0].Trim()), salida);
            //salida.tiempo = CalcularCosto(salida);
           
            
            return CalcularCosto(salida);

        }

        // =========================== ### ===========================
        // calculo de datos para facturacion
        // =========================== ### ===========================

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
        private InfSalidaVehiculo CalcularCosto(vehiculo vehiculo)
        {

            double valorACobrar = 0;
            Costo costo = HttpSolicitudes.getById<Costo>(_urlCosto, 1);

            var fi = DateTime.Parse(vehiculo.fechaI + " " + vehiculo.horaI);
            var fo = DateTime.Parse(vehiculo.fechaO + " " + vehiculo.horaO);

            var Htotales = (fo - fi).ToString(@"dd\d\ hh\h\ mm\m\ ");
            string[] valores = Htotales.Split(" ");

            //diferencia entre horas nomales y nocturnas

            //calcular horas
            int hours = Int32.Parse(valores[1].Substring(0, 2));
            //calcular dias
            int days = Int32.Parse(valores[0].Substring(0, 2));

            int totalhoras = days * 24 + hours;
            int horasNocturnas = tiempoNocturno(vehiculo);
            int horasDiurnas = totalhoras - horasNocturnas;
            //suma de horas por su costo 

            valorACobrar += horasNocturnas * costo.nocturno;
            valorACobrar += horasDiurnas * costo.hora;

            //fracion en minutos

            int min = Int32.Parse( valores[2].Substring(0, 2));

            if (min < 59 && min > 30)
            {
                if (costo.hora > (costo.f15 + costo.f30))
                    valorACobrar += costo.f15 + costo.f30;
                else 
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

            //lleno mi dto

            InfSalidaVehiculo endObj = new InfSalidaVehiculo()
            {
                tiempoTotal = Htotales,
                horasDiurnas = horasDiurnas,
                horasNocturnas = horasNocturnas,
                MontoAPagar = valorACobrar
            };



            //"tiempo": "01d 05h 07m "
            return endObj;
        }
        /// <summary>
        /// tiempo en horas nocturnas
        /// </summary>
        /// <param name="vehiculo"></param>
        /// <returns></returns>
        private int tiempoNocturno(vehiculo vehiculo)
        {

            int Inoche = 20;//la hora se coloca en formato militar 20 es igual a 8 de la noche esta es la hora de inicio del turno nocturno
            int Fnoche = 7;//la hora se coloca en formato militar 7 es igual a 7 de la mañana  esta es la hora de fin del turno nocturno
            int contadorHoras = 0;

            int bandera = 0;

            string[] fechaI = vehiculo.fechaI.Split("/");
            string[] fechaO = vehiculo.fechaO.Split("/");
            string[] horaI = vehiculo.horaI.Split(":");
            string[] horaO = vehiculo.horaO.Split(":");


            int dI = Int32.Parse(fechaI[0]);
            int mI = Int32.Parse(fechaI[1]);
            int yI = Int32.Parse(fechaI[2]);
            int hI=  Int32.Parse(horaI[0]) ;
            int minI=  Int32.Parse(horaI[1]) ;
            int dO = Int32.Parse(fechaO[0]);
            int mO = Int32.Parse(fechaO[1]);
            int yO = Int32.Parse(fechaO[2]);
            int hO = Int32.Parse(horaO[0]);
            int minO = Int32.Parse(horaO[1]);


            if(yI == yO)
            {
                if(mI == mO)
                {
                    if(dI == dO)
                    {
                        if (hO > Inoche)
                            contadorHoras = hO - Inoche;
                    }//si dia de ingreso es diferente al dia de salida
                    else
                    {
                        contadorHoras += 4;
                        if (hO > Inoche)
                            contadorHoras = hO - Inoche;
                        if(hO < Fnoche)
                            contadorHoras =Fnoche - hO;

                        if (dI - dO > 1 )
                        {
                            contadorHoras += 4 * (dI - (dO -1));
                            //si los dias no son continuos sumas las 4 horas de cada dias exta
                        }
                    }
                }//si meses son iguales
                else
                {
                    if (hO > Inoche)
                      contadorHoras += hO - Inoche;
                    //fecbrero
                        if(mI == 2)
                        {
                            if (DateTime.IsLeapYear(yI))
                            {
                                bandera = (29 - dI) + (dO - 1);

                                contadorHoras += bandera * (Inoche - 24 + Fnoche);

                                bandera = 0;
                            }
                            else
                            {
                                bandera = (28 - dI) + (dO - 1);

                                contadorHoras += bandera * (Inoche - 24 + Fnoche);

                                bandera = 0;
                            }

                        }
                      //meses de 30 dias
                    if(mI == 4 || mI == 6 || mI == 9  || mI == 11)
                    {
                        bandera = (30 - dI) + (dO - 1);

                        contadorHoras += bandera * (Inoche - 24 + Fnoche);

                        bandera = 0;
                    }
                    //meses de 31 dias
                    if(mI == 1 || mI == 3 || mI == 5 || mI == 7 || mI == 8 || mI ==10 || mI == 12)
                    {
                        bandera = (31 - dI) + (dO - 1);

                        contadorHoras += bandera * (Inoche - 24 + Fnoche);

                        bandera = 0;
                    }

                }

            }//si años son iguales
            else
            {
                contadorHoras = 0;
                // calculo todo para los dias restantes hasta culminar el mes de inicio
                if (mI == 2)
                {
                    if (DateTime.IsLeapYear(yI))
                    {
                        

                        contadorHoras += (29 - dI) * (Inoche - 24 + Fnoche);

                    }
                    else
                    {

                        contadorHoras += (28 - dI) * (Inoche - 24 + Fnoche);

                    }

                }
                //meses de 30 dias
                if (mI == 4 || mI == 6 || mI == 9 || mI == 11)
                {
                   

                    contadorHoras += (30 - dI) * (Inoche - 24 + Fnoche);

                  
                }
                //meses de 31 dias
                if (mI == 1 || mI == 3 || mI == 5 || mI == 7 || mI == 8 || mI == 10 || mI == 12)
                {
                   

                    contadorHoras += (31 - dI) * (Inoche - 24 + Fnoche);

                  
                }

                //verifico cuantos dias o horas quedan del año inicial

                if( 12 >= (mI+1))
                {
                   for (int t = (mI+1); t<12; t++)
                    {
                        if (t == 2)
                        {
                            if (DateTime.IsLeapYear(yI))
                            {


                                contadorHoras += 29 * (Inoche - 24 + Fnoche);

                            }
                            else
                            {

                                contadorHoras += 28 * (Inoche - 24 + Fnoche);

                            }

                        }
                        //meses de 30 dias
                        if (t == 4 || t == 6 || t == 9 || t == 11)
                        {


                            contadorHoras += 30  * (Inoche - 24 + Fnoche);


                        }
                        //meses de 31 dias
                        if (t == 1 || t == 3 || t == 5 || t == 7 || t == 8 || t == 10 || t == 12)
                        {


                            contadorHoras += 31 * (Inoche - 24 + Fnoche);


                        }
                    }
                }

                //verificamos si hay mas de un año de diferencia

                //aqui esa solo un año
                if(yI != (yO - 1))
                {
                    for( int i = yI; i<yO; i++)
                    {
                        for (int t = 1; t < 12; t++)
                        {
                            if (t == 2)
                            {
                                if (DateTime.IsLeapYear(yI))
                                {


                                    contadorHoras += 29 * (Inoche - 24 + Fnoche);

                                }
                                else
                                {

                                    contadorHoras += 28 * (Inoche - 24 + Fnoche);

                                }

                            }
                            //meses de 30 dias
                            if (t == 4 || t == 6 || t == 9 || t == 11)
                            {


                                contadorHoras += 30 * (Inoche - 24 + Fnoche);


                            }
                            //meses de 31 dias
                            if (t == 1 || t == 3 || t == 5 || t == 7 || t == 8 || t == 10 || t == 12)
                            {


                                contadorHoras += 31 * (Inoche - 24 + Fnoche);


                            }

                        }
                    }

                }
                
                //y aqui simamos las horas contenidas en el ultimo año

                //primeros los meses que sean menores al ultimo mes o al actual

                    for (int t = 1; t < mO; t++)
                    {
                        if (t == 2)
                        {
                            if (DateTime.IsLeapYear(yO))
                            {


                                contadorHoras += 29 * (Inoche - 24 + Fnoche);

                            }
                            else
                            {

                                contadorHoras += 28 * (Inoche - 24 + Fnoche);

                            }

                        }
                        //meses de 30 dias
                        if (t == 4 || t == 6 || t == 9 || t == 11)
                        {


                            contadorHoras += 30 * (Inoche - 24 + Fnoche);


                        }
                        //meses de 31 dias
                        if (t == 1 || t == 3 || t == 5 || t == 7 || t == 8 || t == 10 || t == 12)
                        {


                            contadorHoras += 31 * (Inoche - 24 + Fnoche);


                        }
                    }

                //por ultimo sumamos el mes propio y los ultimos dias y/horas

                contadorHoras += (dO-1) * (Inoche - 24 + Fnoche);

                if (hO > Inoche)
                    contadorHoras += Inoche - hO;
                if (hO < Fnoche)
                    contadorHoras += hO - Fnoche;

            }



            return contadorHoras;
        }

    }
}
