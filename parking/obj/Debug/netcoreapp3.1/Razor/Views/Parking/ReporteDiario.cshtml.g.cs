#pragma checksum "D:\Proyectos con Luis\proyecto3Simple\parking\parking\Views\Parking\ReporteDiario.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "196a2718ca0d3b4d4d5a2e3c698864807a80f64f"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Parking_ReporteDiario), @"mvc.1.0.view", @"/Views/Parking/ReporteDiario.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "D:\Proyectos con Luis\proyecto3Simple\parking\parking\Views\_ViewImports.cshtml"
using parking;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Proyectos con Luis\proyecto3Simple\parking\parking\Views\_ViewImports.cshtml"
using parking.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"196a2718ca0d3b4d4d5a2e3c698864807a80f64f", @"/Views/Parking/ReporteDiario.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"475a75cafecf126d235e3a54ea88445d6a2050e2", @"/Views/_ViewImports.cshtml")]
    public class Views_Parking_ReporteDiario : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<parking.DataTransferObjects.ReporteDiario>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "D:\Proyectos con Luis\proyecto3Simple\parking\parking\Views\Parking\ReporteDiario.cshtml"
  
    ViewData["Title"] = "ReporteDiario";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n<div class=\"container\">\r\n    <hr />\r\n    <div class=\"row\">\r\n      \r\n        <div class=\"col-12\">\r\n                <h2 class=\"control-label formulario__label text-center\"> Reporte hecho el ");
#nullable restore
#line 13 "D:\Proyectos con Luis\proyecto3Simple\parking\parking\Views\Parking\ReporteDiario.cshtml"
                                                                                     Write(DateTime.Now.ToString("dd/MM/yyyy"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"  </h2>
                <div class=""formulario"" id=""formulario"">
                    <!--botones y busquedas -->
                        <div class=""form-group ormulario__grupo formulario__grupo-btn-enviar"">

                            <div class=""contFechas"">
                                <div class=""fecha1"">

                                    <label class=""labelFecha control-label formulario__label text-center"">Desde </label>
                                    <input type=""date"" id=""fechaI""");
            BeginWriteAttribute("value", " value=\"", 841, "\"", 885, 1);
#nullable restore
#line 22 "D:\Proyectos con Luis\proyecto3Simple\parking\parking\Views\Parking\ReporteDiario.cshtml"
WriteAttributeValue("", 849, DateTime.Now.ToString("yyyy-MM-dd"), 849, 36, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" min=\"2020-11-20\"");
            BeginWriteAttribute("max", " max=\"", 903, "\"", 945, 1);
#nullable restore
#line 22 "D:\Proyectos con Luis\proyecto3Simple\parking\parking\Views\Parking\ReporteDiario.cshtml"
WriteAttributeValue("", 909, DateTime.Now.ToString("yyyy-MM-dd"), 909, 36, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@" />
                                </div>
                                <div class=""fecha2"">
                                    <label class=""labelFecha control-label formulario__label text-center"">Hasta </label>
                                    <input type=""date"" id=""fechaO""");
            BeginWriteAttribute("value", " value=\"", 1233, "\"", 1277, 1);
#nullable restore
#line 26 "D:\Proyectos con Luis\proyecto3Simple\parking\parking\Views\Parking\ReporteDiario.cshtml"
WriteAttributeValue("", 1241, DateTime.Now.ToString("yyyy-MM-dd"), 1241, 36, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" min=\"2020-11-20\"");
            BeginWriteAttribute("max", " max=\"", 1295, "\"", 1337, 1);
#nullable restore
#line 26 "D:\Proyectos con Luis\proyecto3Simple\parking\parking\Views\Parking\ReporteDiario.cshtml"
WriteAttributeValue("", 1301, DateTime.Now.ToString("yyyy-MM-dd"), 1301, 36, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" />\r\n                                </div>\r\n                                <div class=\"fecha3 btnFechas\">\r\n\r\n\r\n                                    <buttom id=\"btnReporteEntreFechas\" class=\"btn btn-info text-light\">Calcular</buttom>\r\n");
            WriteLiteral(@"                                    <buttom  id=""btnReporteEntreFechasPDF"" class=""formulario__btnU btn btn-info""> PDF </buttom>
                                    <buttom  id=""btnGenerarReporteEntreFechasXml"" class=""formulario__btnU btn btn-info""> XML </buttom>
                                </div>
                            </div>
                        </div>
                    <!-- valores a buscar-->
                    <div class=""form-group formulario__grupo"">
                        <label class=""control-label formulario__label text-center"">   ");
#nullable restore
#line 40 "D:\Proyectos con Luis\proyecto3Simple\parking\parking\Views\Parking\ReporteDiario.cshtml"
                                                                                 Write(Html.DisplayNameFor(model => model.VehiculosIngresadosHoy));

#line default
#line hidden
#nullable disable
            WriteLiteral("</label>\r\n                        <div class=\"formulario__grupo-input\">\r\n                            <h3 id=\"VehiculosIngresadosHoy\" class=\"text-center\">  ");
#nullable restore
#line 42 "D:\Proyectos con Luis\proyecto3Simple\parking\parking\Views\Parking\ReporteDiario.cshtml"
                                                                             Write(Html.DisplayFor(model => model.VehiculosIngresadosHoy));

#line default
#line hidden
#nullable disable
            WriteLiteral(" </h3>\r\n                        </div>\r\n                    </div>\r\n\r\n                    <div class=\"form-group formulario__grupo\">\r\n                        <label class=\"control-label formulario__label text-center\">    ");
#nullable restore
#line 47 "D:\Proyectos con Luis\proyecto3Simple\parking\parking\Views\Parking\ReporteDiario.cshtml"
                                                                                  Write(Html.DisplayNameFor(model => model.totalVehiculos));

#line default
#line hidden
#nullable disable
            WriteLiteral("  </label>\r\n                        <div class=\"formulario__grupo-input\">\r\n                            <h3 id=\"totalVehiculos\" class=\"text-center\">    ");
#nullable restore
#line 49 "D:\Proyectos con Luis\proyecto3Simple\parking\parking\Views\Parking\ReporteDiario.cshtml"
                                                                       Write(Html.DisplayFor(model => model.totalVehiculos));

#line default
#line hidden
#nullable disable
            WriteLiteral("  </h3>\r\n                        </div>\r\n                    </div>\r\n\r\n                    <div class=\"form-group formulario__grupo\">\r\n                        <label class=\"control-label formulario__label text-center\">     ");
#nullable restore
#line 54 "D:\Proyectos con Luis\proyecto3Simple\parking\parking\Views\Parking\ReporteDiario.cshtml"
                                                                                   Write(Html.DisplayNameFor(model => model.VehiculosQueHanSalido));

#line default
#line hidden
#nullable disable
            WriteLiteral("  </label>\r\n                        <div class=\"formulario__grupo-input\">\r\n                            <h3 id=\"VehiculosQueHanSalido\" class=\"text-center\">                  ");
#nullable restore
#line 56 "D:\Proyectos con Luis\proyecto3Simple\parking\parking\Views\Parking\ReporteDiario.cshtml"
                                                                                            Write(Html.DisplayFor(model => model.VehiculosQueHanSalido));

#line default
#line hidden
#nullable disable
            WriteLiteral("  </h3>\r\n                        </div>\r\n                    </div>\r\n\r\n                    <div class=\"form-group formulario__grupo\">\r\n                        <label class=\"control-label formulario__label text-center\">     ");
#nullable restore
#line 61 "D:\Proyectos con Luis\proyecto3Simple\parking\parking\Views\Parking\ReporteDiario.cshtml"
                                                                                   Write(Html.DisplayNameFor(model => model.VehiculosQueNoHanSalido));

#line default
#line hidden
#nullable disable
            WriteLiteral("  </label>\r\n                        <div class=\"formulario__grupo-input\">\r\n                            <h3 id=\"VehiculosQueNoHanSalido\" class=\"text-center\">                  ");
#nullable restore
#line 63 "D:\Proyectos con Luis\proyecto3Simple\parking\parking\Views\Parking\ReporteDiario.cshtml"
                                                                                              Write(Html.DisplayFor(model => model.VehiculosQueNoHanSalido));

#line default
#line hidden
#nullable disable
            WriteLiteral("  </h3>\r\n                        </div>\r\n                    </div>\r\n\r\n                    <div class=\"form-group formulario__grupo\">\r\n                        <label class=\"control-label formulario__label text-center\">\r\n                            ");
#nullable restore
#line 69 "D:\Proyectos con Luis\proyecto3Simple\parking\parking\Views\Parking\ReporteDiario.cshtml"
                       Write(Html.DisplayNameFor(model => model.cantidadDineroHoy));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </label>\r\n                        <div class=\"formulario__grupo-input\">\r\n                            <h2 id=\"cantidadDineroHoy\" class=\"text-center\">    ");
#nullable restore
#line 72 "D:\Proyectos con Luis\proyecto3Simple\parking\parking\Views\Parking\ReporteDiario.cshtml"
                                                                          Write(Html.DisplayFor(model => model.cantidadDineroHoy));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"  S/. </h2>
                        </div>
                    </div>



                </div>
        </div>

    </div>
    <hr />

    <div class=""row"">
        <div class=""col-12"">
            <h2 class=""control-label formulario__label text-center""> Montos Usados Para Calcular  </h2>

            <div class=""formulario"" id=""formulario"">
                <div class=""form-group formulario__grupo "">

                    <div class=""formulario__grupo-input"">
                        <label class=""control-label formulario__label text-center""> Nocturno  </label>
                        <h4 class=""text-center"">    ");
#nullable restore
#line 93 "D:\Proyectos con Luis\proyecto3Simple\parking\parking\Views\Parking\ReporteDiario.cshtml"
                                               Write(Html.DisplayFor(model => model.costosUsados.nocturno));

#line default
#line hidden
#nullable disable
            WriteLiteral(@" S/. </h4>
                    </div>
                </div>
                <div class=""form-group formulario__grupo"">
                    <div class=""formulario__grupo-input"">
                        <label class=""control-label formulario__label text-center""> Hora  </label>
                        <h4 class=""text-center"">    ");
#nullable restore
#line 99 "D:\Proyectos con Luis\proyecto3Simple\parking\parking\Views\Parking\ReporteDiario.cshtml"
                                               Write(Html.DisplayFor(model => model.costosUsados.hora));

#line default
#line hidden
#nullable disable
            WriteLiteral(@" S/. </h4>
                    </div>
                </div>
                <div class=""form-group formulario__grupo"">
                    <div class=""formulario__grupo-input"">
                        <label class=""control-label formulario__label text-center""> fracion 30 in  </label>
                        <h4 class=""text-center"">    ");
#nullable restore
#line 105 "D:\Proyectos con Luis\proyecto3Simple\parking\parking\Views\Parking\ReporteDiario.cshtml"
                                               Write(Html.DisplayFor(model => model.costosUsados.f30));

#line default
#line hidden
#nullable disable
            WriteLiteral(@" S/. </h4>
                    </div>
                </div>
                <div class=""form-group formulario__grupo"">
                    <div class=""formulario__grupo-input"">
                        <label class=""control-label formulario__label text-center""> fraccion 15 min  </label>
                        <h4 class=""text-center"">    ");
#nullable restore
#line 111 "D:\Proyectos con Luis\proyecto3Simple\parking\parking\Views\Parking\ReporteDiario.cshtml"
                                               Write(Html.DisplayFor(model => model.costosUsados.f15));

#line default
#line hidden
#nullable disable
            WriteLiteral(@" S/. </h4>
                    </div>
                </div>
                <div class=""form-group formulario__grupo"">
                    <div class=""formulario__grupo-input"">
                        <label class=""control-label formulario__label text-center""> fraccion 5 minutos  </label>
                        <h4 class=""text-center"">    ");
#nullable restore
#line 117 "D:\Proyectos con Luis\proyecto3Simple\parking\parking\Views\Parking\ReporteDiario.cshtml"
                                               Write(Html.DisplayFor(model => model.costosUsados.f5));

#line default
#line hidden
#nullable disable
            WriteLiteral(@" S/. </h4>
                    </div>
                </div>
            </div>
        </div>


        </div>
</div>


<script>


    const btnReporteEntreFechas = document.getElementById(""btnReporteEntreFechas"");
    const btnReporteEntreFechasPDF = document.getElementById('btnReporteEntreFechasPDF');
    const btnGenerarReporteEntreFechasXml = document.getElementById('btnGenerarReporteEntreFechasXml');

    // ============ ### ============
    // Organizo las fechas para que me las entienda C#
    // ============ ### ============

    const cambiarFormatoFecha = fecha => {

        if (fecha == '')
            return '';

        let band = fecha.split('-'); // != null ? fecha.split('/') : fecha.split('-');
        let flag = band[0];
        band[0] = band[2];
        band[2] = flag;

        return band.join('/');

    }

    // ============ ### ============
    // introduzco el boton de click
    // ============ ### ============
    btnReporteEntreFechas.addEventL");
            WriteLiteral(@"istener('click', () => {
        let fi = document.getElementById(""fechaI"").value;
        let fo = document.getElementById(""fechaO"").value


        fi = cambiarFormatoFecha(fi);
        fo = cambiarFormatoFecha(fo);

        console.log(fi + "" "" + fo);


        $.get(`ReporteEntreFechas?fechaI=${fi}&fechaO=${fo}`, data => {
            console.log(data);

            if (data != null) {


                document.getElementById('VehiculosIngresadosHoy').innerHTML = '<h3 id=""VehiculosIngresadosHoy"" class=""text-center"">' + data.vehiculosIngresadosHoy + '</h3>';
                document.getElementById('totalVehiculos').innerHTML = '<h3 id=""totalVehiculos"" class=""text-center"">' + data.totalVehiculos + '</h3>';
                document.getElementById('VehiculosQueHanSalido').innerHTML = '<h3 id=""VehiculosQueHanSalido"" class=""text-center"">' + data.vehiculosQueHanSalido + '</h3>';
                document.getElementById('VehiculosQueNoHanSalido').innerHTML = '<h3 id=""VehiculosQueNoHanSalido"" c");
            WriteLiteral(@"lass=""text-center"">' + data.vehiculosQueNoHanSalido + '</h3>';
                document.getElementById('cantidadDineroHoy').innerHTML = '  <h2 id=""cantidadDineroHoy"" class=""text-center"">' + data.cantidadDineroHoy + 'S/. </h2>';
            }


        });

    });

    btnReporteEntreFechasPDF.addEventListener('click', () => {
        let fi = document.getElementById(""fechaI"").value;
        let fo = document.getElementById(""fechaO"").value


        fi = cambiarFormatoFecha(fi);
        fo = cambiarFormatoFecha(fo);

        console.log(fi + "" "" + fo);

        window.open(`ReporteEntreFechasPDF?fechaI=${fi}&fechaO=${fo}`, '_blank');

");
            WriteLiteral(@"
    });

    btnGenerarReporteEntreFechasXml.addEventListener('click', () => {

       

        let fi = document.getElementById(""fechaI"").value;
        let fo = document.getElementById(""fechaO"").value


        fi = cambiarFormatoFecha(fi);
        fo = cambiarFormatoFecha(fo);

        console.log(fi + "" "" + fo);

        var a = document.createElement(""a"");
        a.href = `GenerarReporteEntreFechasXml?fechaI=${fi}&fechaO=${fo}`;
        a.download = ""reporte.xml"";
        a.click();

      


    });



</script>");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<parking.DataTransferObjects.ReporteDiario> Html { get; private set; }
    }
}
#pragma warning restore 1591
