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
using parking.DataTransferObjects;
using OfficeOpenXml;

namespace parking.Controllers
{
    public class ParkingController : Controller
    {

        #region declaraciones Iniciales

        /// <summary>
        /// url de la unbicacion de mi api rest
        /// </summary>
        private readonly string _url = "https://localhost:44306/API/parkingAPI";
        /// <summary>
        /// par extraer el valor de mi costo desde database
        /// </summary>
        private readonly string _urlCosto = "https://localhost:44306/API/CostoApi";


        #endregion


        #region Vistas
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
        /// vista donde se retornan los datos del pago final del vehiculo
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        //public IActionResult SalidaVehicular(string texto)
        public IActionResult SalidaVehicular()
        {
            
                return View();
            
        }
        #endregion

        #region reportes
        // =========================== ### ===========================
        // Reportes Diaros
        // =========================== ### ===========================

        /// <summary>
        /// me genera un reporte dandole dos fechas como comun
        /// </summary>
        /// <param name="fechaI"></param>
        /// <param name="fechaO"></param>
        /// <returns></returns>
        public ReporteDiario ReporteEntreFechas(string fechaI, string fechaO)
       {
            //{{url}}API/parkingAPI/

            try
            {
                var lista = HttpSolicitudes.GetList<vehiculo>(_url + "/todo/fechas?fechaI="+fechaI+"&fechaO="+fechaO);
                return ContadorReportes(lista, false);
            }
            catch
            {
                ViewBag.error = "Algo Anda Mal Con Las Fechas";
                return null;
            }


       }
        


        /// <summary>
        /// vista de mis reportes diarios, 
        /// se ingresan dos fechas y este hace el calculo en base a estas dos fechas
        /// en caso de que las fechas ingresadas sea invalidas retorna el reporte del dia actual
        /// si el error es a nivel de excepcion se retorna el elemento vacio
        /// </summary>
        /// <returns></returns>
        public IActionResult ReporteDiario(string fechaI, string fechaO)
        {


            try
            {
                if(String.IsNullOrEmpty(fechaI) || String.IsNullOrEmpty(fechaO))
                {
                    List<vehiculo> lista = HttpSolicitudes.GetList<vehiculo>(_url + "/todo");
                    return View(ContadorReportes(lista));

                }
                else
                {
                    List<vehiculo> lista = HttpSolicitudes.GetList<vehiculo>(_url + "/todo/fechas?fechaI=" + fechaI + "&fechaO=" + fechaO);
                    return View(ContadorReportes(lista,false));
                }


            }
            catch
            {
                ViewBag.error = "Algo Va Mal Con La Base De Datos";
                return View(NoContent());
            }


        }
        /// <summary>
        /// retorna el DTO de los reportes generados
        /// </summary>
        /// <param name="lista"></param>
        /// <returns></returns>
        private ReporteDiario ContadorReportes(List<vehiculo> lista, bool actual = true)
        {
            try
            {
                if (actual)
                {
                    return new ReporteDiario()
                    {
                        totalVehiculos = lista.Count(),
                        VehiculosQueNoHanSalido = lista.FindAll(pre => pre.fechaO == null).Count(),
                        VehiculosIngresadosHoy = lista.FindAll(pre => pre.fechaI == DateTime.Now.ToString("dd/MM/yyyy")).Count(),
                        VehiculosQueHanSalido = lista.FindAll(pre => pre.fechaO == DateTime.Now.ToString("dd/MM/yyyy")).Count(),
                        costosUsados = HttpSolicitudes.getById<Costo>(_urlCosto, 1),
                        cantidadDineroHoy = calcularMontosPorDia(lista, actual)
                    };
                }
                else
                {
                    return new ReporteDiario()
                    {
                        totalVehiculos = lista.Count(),
                        VehiculosQueHanSalido = lista.FindAll(v => v.fechaO != null).Count(),
                        VehiculosQueNoHanSalido = lista.FindAll(v => v.fechaO == null).Count(),
                        VehiculosIngresadosHoy = lista.FindAll(v => v.fechaO == DateTime.Now.ToString("dd/MM/yyyy")).Count,
                        costosUsados = HttpSolicitudes.getById<Costo>(_urlCosto, 1),
                        cantidadDineroHoy = calcularMontosPorDia(lista, actual)
                    };
                }

            }
            catch
            {
                ViewBag.error = "Algo Anda Mal Con Las Base de datos";
                return null;
            }



        }

        /// <summary>
        /// calcula los montos en el dia actual y retorna toda la sumatoria
        /// </summary>
        /// <param name="lista"></param>
        /// <returns></returns>

        private double calcularMontosPorDia(List<vehiculo> lista,bool actual = true)
        {

            try
            {
                double total = 0;
                InfSalidaVehiculo band = new InfSalidaVehiculo();

                if (actual)
                {
                    //double total = 0;
                    foreach (var item in lista)
                    {

                        if (item.fechaO == DateTime.Now.ToString("dd/MM/yyyy"))
                        {
                            band = CalcularCosto(item);
                            total += band.MontoAPagar;
                            band = null;
                        }

                    }
                }
                else
                {
                    foreach (var item in lista)
                    {

                        if (item.fechaO != null)
                        {
                            band = CalcularCosto(item);
                            total += band.MontoAPagar;
                            band = null;
                        }

                    }
                }
                return total;
            }
            catch
            {
                ViewBag.error = "Algo Anda Mal Con Las Base de datos";
                return 0;
            }
        }

        #endregion

        #region impreciones
        // =========================== ### ===========================
        // impresiones en PDF
        // =========================== ### ===========================


        public FileResult ReporteEntreFechasPDF(string fechaI,string fechaO)
        {
            ReporteDiario reporte = ReporteEntreFechas(fechaI, fechaO);

            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    PdfWriter writer = new PdfWriter(ms);
                    using (var pdfDoc = new PdfDocument(writer))
                    {
                        Document doc = new Document(pdfDoc, PageSize.A7);
                        doc.SetMargins(10, 10, 0, 10);
                        //agrego datos
                        Paragraph c1 = new Paragraph("Reporte del " + fechaI + " Hasta " + fechaO);
                        Paragraph c2 = new Paragraph("Historico Vehiculos: " + reporte.totalVehiculos);
                        Paragraph c3 = new Paragraph("Vehiculos ingresados: " + reporte.VehiculosIngresadosHoy);
                        Paragraph c4 = new Paragraph("Vehiculos que han salido: " + reporte.VehiculosQueHanSalido);
                        Paragraph c5 = new Paragraph("Vehiculos que aun se encuentran estacionados: " + reporte.VehiculosQueNoHanSalido);
                        Paragraph c6 = new Paragraph("Dinero recaudado: " + reporte.cantidadDineroHoy + "S/.");

                        //agrego estilos
                        c1.SetFontSize(15);
                        c1.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
                        c2.SetFontSize(10);
                        c2.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
                        c3.SetFontSize(10);
                        c3.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
                        c4.SetFontSize(10);
                        c4.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
                        c5.SetFontSize(10);
                        c5.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
                        c6.SetFontSize(10);
                        c6.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
                        //agrego al documento
                        doc.Add(c1);
                        doc.Add(c2);
                        doc.Add(c3);
                        doc.Add(c4);
                        doc.Add(c5);
                        doc.Add(c6);
                        //cierro para enviar
                        doc.Close();
                        writer.Close();
                    }
                    return File(ms.ToArray(), "application/pdf");
                }

            }
            catch
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    PdfWriter writer = new PdfWriter(ms);
                    using (var pdfDoc = new PdfDocument(writer))
                    {
                        Document doc = new Document(pdfDoc, PageSize.A7);
                        doc.SetMargins(10, 10, 0, 10);
                        //agrego datos
                        Paragraph c1 = new Paragraph("Datos Suministrados No Validos");
                        

                        //agrego estilos
                        c1.SetFontSize(15);
                        c1.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
                       

                        //agrego al documento
                        doc.Add(c1);
                      
                        //cierro para enviar
                        doc.Close();
                        writer.Close();
                    }
                    return File(ms.ToArray(), "application/pdf");
                }
            }


        }

        /// <summary>
        /// para imprimir los datos del vehiculo que va de salida
        /// aparte de mostrarlo en pantella hay que generar este recibo
        /// </summary>
        /// <returns></returns>

        public FileResult salidaVehicularPDF(InfSalidaVehiculo inf)
        {
            //InfSalidaVehiculo inf = DevolverSalida(id.ToString() + "=>xx/xx/xxx=>HH:mm");
            //InfSalidaVehiculo inf = DevolverSalida(textoQR);
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    PdfWriter writer = new PdfWriter(ms);
                    using (var pdfDoc = new PdfDocument(writer))
                    {
                        Document doc = new Document(pdfDoc, PageSize.A7);
                        doc.SetMargins(10, 10, 0, 10);
                        //agrego datos
                        Paragraph c1 = new Paragraph("Salida Vehicular");
                        Paragraph c2 = new Paragraph("Tiempo Total: " + inf.tiempoTotal);
                        Paragraph c6 = new Paragraph("Horas Sab/Dom: " + inf.horasFinDeSemana);

                        Paragraph c3 = new Paragraph("horas Diurnas: " + inf.horasDiurnas);
                        Paragraph c4 = new Paragraph("Horas Nocturnas: " + inf.horasNocturnas);
                        Paragraph c5 = new Paragraph("Monto A Pagar: " + inf.MontoAPagar);

                        //agrego estilos
                        c1.SetFontSize(15);
                        c1.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
                        c2.SetFontSize(10);
                        c2.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
                        c6.SetFontSize(10);
                        c6.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
                        c3.SetFontSize(10);
                        c3.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
                        c4.SetFontSize(10);
                        c4.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
                        c5.SetFontSize(20);
                        c5.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);

                        //agrego al documento
                        doc.Add(c1);
                        doc.Add(c2);
                        doc.Add(c6);
                        doc.Add(c3);
                        doc.Add(c4);
                        doc.Add(c5);
                        //cierro para enviar
                        doc.Close();
                        writer.Close();
                    }
                    return File(ms.ToArray(), "application/pdf");
                }
            }
            catch
            {
                ViewBag.error = "Algo Anda Mal Con Las Base de datos";
                return null;
            }
        }
        /// <summary>
        /// para devolver el reporte diario en PDF
        /// </summary>
        /// <returns></returns>
        public FileResult ReporteDiarioPDF()
        {
            try
            {

                List<vehiculo> lista = HttpSolicitudes.GetList<vehiculo>(_url + "/todo");


                ReporteDiario reporte = ContadorReportes(lista);



                using (MemoryStream ms = new MemoryStream())
                {
                    PdfWriter writer = new PdfWriter(ms);
                    using (var pdfDoc = new PdfDocument(writer))
                    {
                        Document doc = new Document(pdfDoc, PageSize.A7);
                        doc.SetMargins(10, 10, 0, 10);
                        //agrego datos
                        Paragraph c1 = new Paragraph("Reporte Diario");
                        Paragraph c2 = new Paragraph("Historico Vehiculos: " + reporte.totalVehiculos);
                        Paragraph c3 = new Paragraph("Vehiculos ingresados el dia de hoy: " + reporte.VehiculosIngresadosHoy);
                        Paragraph c4 = new Paragraph("Vehiculos que han salido el dia de hoy: " + reporte.VehiculosQueHanSalido);
                        Paragraph c5 = new Paragraph("Vehiculos que aun se encuentran estacionados: " + reporte.VehiculosQueNoHanSalido);
                        Paragraph c6 = new Paragraph("Dinero recaudado El dia de hoy: " + reporte.cantidadDineroHoy + "S/.");

                        //agrego estilos
                        c1.SetFontSize(15);
                        c1.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
                        c2.SetFontSize(10);
                        c2.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
                        c3.SetFontSize(10);
                        c3.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
                        c4.SetFontSize(10);
                        c4.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
                        c5.SetFontSize(10);
                        c5.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
                        c6.SetFontSize(10);
                        c6.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
                        //agrego al documento
                        doc.Add(c1);
                        doc.Add(c2);
                        doc.Add(c3);
                        doc.Add(c4);
                        doc.Add(c5);
                        doc.Add(c6);
                        //cierro para enviar
                        doc.Close();
                        writer.Close();
                    }
                    return File(ms.ToArray(), "application/pdf");
                }

            }
            catch
            {
                ViewBag.error = "Algo Anda Mal Con Las Base de datos";
                return null;
            }


        }

        /// <summary>
        /// con los datos actuales retorno un QR
        /// este Qr se realiza con los nuevos datos del vehiculo
        /// </summary>
        /// <returns></returns>
        public FileResult DevolverQR()
        {
            try
            {

                vehiculo nuevo = new vehiculo()
                {
                    id = 1,
                    costo = 1,
                    fechaI = DateTime.Now.ToString("dd/MM/yyyy"),
                    horaI = DateTime.Now.ToString("t")

                };

                nuevo = HttpSolicitudes.PostandGetHTTP<vehiculo>(_url + "/insertarDatos", nuevo);

                return QRenPDF(nuevo);
            }
            catch
            {
                ViewBag.error = "Algo Anda Mal Con Las Base de datos";
                return null;
            }
        }

        // =========================== ### ===========================
        // imprecion en Xml o Excel
        // =========================== ### ===========================

        public FileResult GenerarReporteEntreFechasXml(string fechaI, string fechaO)
        {

            try {

                List<vehiculo> Lista = HttpSolicitudes.GetList<vehiculo>(_url + "/todo/fechas?fechaI=" + fechaI + "&fechaO=" + fechaO);

                string[] NombrePropiedades = new string[] { "id", "costo", "fechaI", "horaI", "fechaO", "horaO" };
                string[] Cabeceras = new string[] { "id", "Costo", "Fecha Ingreso", "Hora Ingreso", "Fecha Salida", "Hora Salida", "Cobrado" };


                using (MemoryStream ms = new MemoryStream())
                {
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    using (ExcelPackage ep = new ExcelPackage())
                    {
                        ep.Workbook.Worksheets.Add("hoja");
                        ExcelWorksheet ew = ep.Workbook.Worksheets[0];
                        //primero irian mis cabeceras
                        for (int i = 0; i < Cabeceras.Length; i++)
                        {
                            ew.Cells[1, i + 1].Value = Cabeceras[i];
                            ew.Column(i + 1).Width = 25;
                        }
                        //ahora vamos con el contenido
                        int fila = 2;
                        int columna = 1;

                        //foreach(var item in lista)

                        foreach (var item in Lista)
                        {
                            columna = 1;
                            foreach (string propiedad in NombrePropiedades)
                            {
                                ew.Cells[fila, columna].Value = item.GetType().GetProperty(propiedad).GetValue(item).ToString();
                                columna++;
                            }
                            ew.Cells[fila, columna].Value = CalcularCosto(item).MontoAPagar.ToString() + " S/.";

                            fila++;
                        }


                        ReporteDiario _rep = ReporteEntreFechas(fechaI, fechaO);



                       
                        fila++;
                        ew.Cells[fila, 1].Value = "Total Vehiculos";
                        ew.Cells[fila, 2].Value = _rep.totalVehiculos.ToString();
                       
                        fila++;
                        ew.Cells[fila, 1].Value = "Total Dinero Recaudado";
                        ew.Cells[fila, 2].Value = _rep.cantidadDineroHoy.ToString()+ " S/.";



                        ep.SaveAs(ms);
                        Byte[] buffer = ms.ToArray();
                        //return buffer;
                        return File(buffer.ToArray(), "application/xml");
                    }
                }
            }
            catch
            {
                ViewBag.error = "problemas en la coneccion sa base de datos";
                return null;
            }
            
            }


        #endregion

        #region calculos
        // =========================== ### ===========================
        // componentes de comunicacion
        // =========================== ### ===========================
        /// <summary>
        /// para generar un pdf con el qr de la informacion necesaria del nuevo ingreso vehicular
        /// </summary>
        /// <param name="nuevo"></param>
        /// <returns></returns>
        public FileResult QRenPDF(vehiculo nuevo)
        {
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    PdfWriter writer = new PdfWriter(ms);
                    using (var pdfDoc = new PdfDocument(writer))
                    {
                        Document doc = new Document(pdfDoc, PageSize.A7);
                        doc.SetMargins(10, 10, 0, 10);
                        //agrego datos
                        Paragraph c1 = new Paragraph("Ingreso Vehicular");
                        Paragraph c2 = new Paragraph("Hora Ingreso: " + nuevo.horaI);
                        Paragraph c3 = new Paragraph("Fecha Ingreso: " + nuevo.fechaI);
                        Paragraph cE = new Paragraph(" _______________ ");
                        Paragraph c4 = new Paragraph("Por Favor NO DOBLE ni ARRUGE este tickete");
                        BarcodeQRCode brQR = new BarcodeQRCode(nuevo.id.ToString() + "=>" + nuevo.fechaI + "=>" + nuevo.horaI);
                        Image imgQR = new Image(brQR.CreateFormXObject(pdfDoc));
                        //agrego estilos
                        c1.SetFontSize(10);
                        c1.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
                        c2.SetFontSize(5);
                        c2.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
                        c3.SetFontSize(5);
                        c3.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
                        cE.SetFontSize(5);
                        cE.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
                        c4.SetFontSize(5);
                        c4.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
                        //imgQR.ScaleToFit()
                        imgQR.ScaleToFit(180, 180);
                        imgQR.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
                        //agrego al documento
                        doc.Add(c1);
                        doc.Add(c2);
                        doc.Add(c3);
                        doc.Add(imgQR);
                        doc.Add(cE);
                        //doc.Add(cE);
                        doc.Add(c4);
                        //cierro para enviar
                        doc.Close();
                        writer.Close();
                    }
                    return File(ms.ToArray(), "application/pdf");
                }
            }
            catch
            {
                ViewBag.error = "Algo Anda Mal Con Las Base de datos";
                return null;
            }
        }


        /// <summary>
        /// aqui retorno los datos del con lo obtenido por el qr
        /// </summary>
        /// <param name="textoQr"></param>

        //public InfSalidaVehiculo DevolverSalida(string textoQr)
        public FileResult DevolverSalida(string texto)
        {
            try
            {

                if (String.IsNullOrEmpty(texto))
                    return null;

                string[] datos = texto.Split("=>");

                vehiculo salida = new vehiculo();
                salida.id = Int32.Parse(datos[0].Trim());
                salida.fechaI = datos[1].Trim();
                salida.horaI = datos[2].Trim();
                salida.fechaO = DateTime.Now.ToString("dd/MM/yyyy");
                salida.horaO = DateTime.Now.ToString("t");

                salida = HttpSolicitudes.PutAndGetHTTP<vehiculo>(_url + "/salida", Int32.Parse(datos[0].Trim()), salida);
                //salida.tiempo = CalcularCosto(salida);


                return salidaVehicularPDF(CalcularCosto(salida));
            }
            catch
            {
                ViewBag.error = "Algo Anda Mal Con Las Base de datos";
                return null;
            }
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
            try
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

                int weekend = HorasFinDeSemanaEntreDosFechas(vehiculo);

                int totalhoras = days * 24 + hours;
                
                int horasNocturnas = (tiempoNocturno(vehiculo) < 0 ) ? 0 : tiempoNocturno(vehiculo);
                int horasDiurnas = (totalhoras - horasNocturnas - weekend) < 0 ? 0 : totalhoras - horasNocturnas - weekend;
                //suma de horas por su costo 

                valorACobrar = (horasNocturnas * costo.nocturno) + (horasDiurnas * costo.hora) + (weekend * costo.weeekend);
                //valorACobrar += horasDiurnas * costo.hora;
                //valorACobrar += weekend * costo.weeekend;

                //fracion en minutos

                int min = Int32.Parse(valores[2].Substring(0, 2));

                if (min < 59 && min > 30)
                {
                    if (costo.hora > (costo.f15 + costo.f30))
                        valorACobrar += costo.f15 + costo.f30;
                    else
                        valorACobrar += costo.hora;
                }
                else if (min > 15 && min <= 30)
                {
                    valorACobrar += costo.f30;
                }
                else if (min > 5 && min <= 15)
                {
                    valorACobrar += costo.f15;
                }
                else if (min <= 5)
                {
                    valorACobrar += costo.f5;
                }

                //lleno mi dto

                InfSalidaVehiculo endObj = new InfSalidaVehiculo()
                {
                    id = vehiculo.id,
                    tiempoTotal = Htotales,
                    horasDiurnas = horasDiurnas,
                    horasNocturnas = horasNocturnas,
                    horasFinDeSemana = weekend,
                    MontoAPagar = valorACobrar
                };



                //"tiempo": "01d 05h 07m "
                return endObj;
            }
            catch
            {
                ViewBag.error = "Algo Anda Mal Con Las Base de datos";
                return null;
            }
        }
        /// <summary>
        /// tiempo en horas nocturnas
        /// </summary>
        /// <param name="vehiculo"></param>
        /// <returns></returns>
        private int tiempoNocturno(vehiculo vehiculo)
        {

            try
            {
                int Inoche = 20;//la hora se coloca en formato militar 20 es igual a 8 de la noche esta es la hora de inicio del turno nocturno
                int Fnoche = 7;//la hora se coloca en formato militar 7 es igual a 7 de la mañana  esta es la hora de fin del turno nocturno
                int contadorHoras = 0;

                int bandera = 0;

                string[] fechaI = vehiculo.fechaI.Split("/") != null ? vehiculo.fechaI.Split("/") : vehiculo.fechaI.Split("-");
                string[] fechaO = vehiculo.fechaO.Split("/") != null ? vehiculo.fechaO.Split("/") : vehiculo.fechaO.Split("-");
                string[] horaI = vehiculo.horaI.Split(":");
                string[] horaO = vehiculo.horaO.Split(":");


                int dI = Int32.Parse(fechaI[0]);
                int mI = Int32.Parse(fechaI[1]);
                int yI = Int32.Parse(fechaI[2]);
                int hI = Int32.Parse(horaI[0]);
                int minI = Int32.Parse(horaI[1]);
                int dO = Int32.Parse(fechaO[0]);
                int mO = Int32.Parse(fechaO[1]);
                int yO = Int32.Parse(fechaO[2]);
                int hO = Int32.Parse(horaO[0]);
                int minO = Int32.Parse(horaO[1]);


                if (yI == yO)
                {
                    if (mI == mO)
                    {
                        if (dI == dO)
                        {
                            if (hO > Inoche)
                                contadorHoras = hO - Inoche;
                        }//si dia de ingreso es diferente al dia de salida
                        else
                        {
                            contadorHoras += 4;
                            if (hO > Inoche)
                                contadorHoras = hO - Inoche;
                            if (hO < Fnoche)
                                contadorHoras = Fnoche - hO;
                            if (hO < Inoche && hO > Fnoche)
                                contadorHoras += Fnoche;

                            if (dI - dO > 1)
                            {
                                contadorHoras += (24 - Inoche + Fnoche) * (dI - (dO - 1));
                                //si los dias no son continuos sumas las 4 horas de cada dias exta
                            }
                        }
                    }//si meses son iguales
                    else
                    {
                        if (hO > Inoche)
                            contadorHoras += hO - Inoche;
                        //fecbrero
                        if (mI == 2)
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
                        if (mI == 4 || mI == 6 || mI == 9 || mI == 11)
                        {
                            bandera = (30 - dI) + (dO - 1);

                            contadorHoras += bandera * (Inoche - 24 + Fnoche);

                            bandera = 0;
                        }
                        //meses de 31 dias
                        if (mI == 1 || mI == 3 || mI == 5 || mI == 7 || mI == 8 || mI == 10 || mI == 12)
                        {
                            bandera = (31 - dI) + (dO - 1);

                            contadorHoras += bandera * (Inoche - 24 + Fnoche);

                            bandera = 0;
                        }
                        //si la salida esta generada en base a la hora de salida
                        if (hO > Inoche)
                            contadorHoras = hO - Inoche;
                        if (hO < Fnoche)
                            contadorHoras = Fnoche - hO;
                        if (hO < Inoche && hO > Fnoche)
                            contadorHoras += Fnoche;
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

                    if (12 >= (mI + 1))
                    {
                        for (int t = (mI + 1); t < 12; t++)
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

                    //verificamos si hay mas de un año de diferencia

                    //aqui esa solo un año
                    if (yI != (yO - 1))
                    {
                        for (int i = yI; i < yO; i++)
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

                    contadorHoras += (dO - 1) * (Inoche - 24 + Fnoche);

                    if (hO > Inoche)
                        contadorHoras += Inoche - hO;
                    if (hO < Fnoche)
                        contadorHoras += hO - Fnoche;

                }



                return contadorHoras;
            }
            catch
            {
                ViewBag.error = "Error Al Obtener Horas Nocturnas";
                return 0;
            }
        }
        #endregion

        #region calculo fines de semana

        private int HorasFinDeSemanaEntreDosFechas(vehiculo vehiculo)
        {

            //DateTime diaI = new DateTime(Int32.Parse(fechaI.Substring(6,4)), Int32.Parse(fechaI.Substring(3, 2)), Int32.Parse(fechaI.Substring(0, 2)));
            //DateTime diaO = new DateTime(Int32.Parse(fechaO.Substring(6,4)), Int32.Parse(fechaO.Substring(3, 2)), Int32.Parse(fechaO.Substring(0, 2) ) );
            string[] diaI = vehiculo.fechaI.Split("-") == null ? vehiculo.fechaI.Split("-") : vehiculo.fechaI.Split('/');
            string[] diaF = vehiculo.fechaO.Split("-") == null ? vehiculo.fechaO.Split("-") : vehiculo.fechaO.Split('/');
            string[] hi = vehiculo.horaI.Split(":");
            string[] ho = vehiculo.horaO.Split(":");


            DateTime inicio = new DateTime(Int32.Parse(diaI[2]), Int32.Parse(diaI[1]), Int32.Parse(diaI[0]), Int32.Parse(hi[0]), Int32.Parse(hi[1]), 0);
            DateTime final = new DateTime(Int32.Parse(diaF[2]), Int32.Parse(diaF[1]), Int32.Parse(diaF[0]),Int32.Parse(ho[0]),Int32.Parse(ho[1]),0);

            int contadoDeDias = 0;
            int contadorDeHoras = 0;

            if (inicio == final) {
                if ((int)inicio.DayOfWeek == 0 || (int)inicio.DayOfWeek == 6)
                    return (inicio - final).Hours;
            }
            else
            {
                DateTime dia = inicio;
                do
                {

                    if ((int)dia.DayOfWeek == 0 || (int)dia.DayOfWeek == 6)
                    {
                        contadoDeDias++;
                        //aqui hay que contar son las horas
                    }

                    dia = dia.AddDays(1);

                } while (dia <= final);

                if ((int)inicio.DayOfWeek == 0 || (int)inicio.DayOfWeek == 6)
                {
                    contadoDeDias--;
                    contadorDeHoras = ((20 - Int32.Parse(hi[0])) > 0) ? (20 - Int32.Parse(hi[0])) : 0;

                }
                if ((int)final.DayOfWeek == 0 || (int)final.DayOfWeek == 6)
                {
                    contadoDeDias--;
                    if(Int32.Parse(ho[0]) < 20  && Int32.Parse(ho[0]) > 7)
                        contadorDeHoras += Int32.Parse(ho[0]) - 7 ;
                }

                contadorDeHoras += contadoDeDias * 13; 


            }

            return contadorDeHoras;

        }

        #endregion


    }
}
