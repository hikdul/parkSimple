#pragma checksum "D:\Proyectos con Luis\proyecto3Simple\parking\parking\Views\Parking\SalidaVehicular.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "fedb523f8d8ae10ad3673126b1708f1ee41133be"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Parking_SalidaVehicular), @"mvc.1.0.view", @"/Views/Parking/SalidaVehicular.cshtml")]
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
#nullable restore
#line 1 "D:\Proyectos con Luis\proyecto3Simple\parking\parking\Views\Parking\SalidaVehicular.cshtml"
using parking.DataTransferObjects;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"fedb523f8d8ae10ad3673126b1708f1ee41133be", @"/Views/Parking/SalidaVehicular.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"475a75cafecf126d235e3a54ea88445d6a2050e2", @"/Views/_ViewImports.cshtml")]
    public class Views_Parking_SalidaVehicular : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<InfSalidaVehiculo>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Parking", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "salidaVehicularPDF", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("target", new global::Microsoft.AspNetCore.Html.HtmlString("_blank"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("formulario__btn btn btn-info"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "parking", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "SalidaVehicular", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 4 "D:\Proyectos con Luis\proyecto3Simple\parking\parking\Views\Parking\SalidaVehicular.cshtml"
  
    ViewData["Title"] = "SalidaVehicular";



#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1>SalidaVehicular</h1>\r\n");
#nullable restore
#line 11 "D:\Proyectos con Luis\proyecto3Simple\parking\parking\Views\Parking\SalidaVehicular.cshtml"
 if (Model != null)
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <div class=\"row\">\r\n");
            WriteLiteral("\r\n        <hr />\r\n");
            WriteLiteral("        <div class=\"col-12\">\r\n            <div class=\"formulario\" id=\"formulario\">\r\n\r\n\r\n\r\n              \r\n\r\n                <div class=\"form-group formulario__grupo\">\r\n                    <label class=\"control-label formulario__label text-center\">   ");
#nullable restore
#line 27 "D:\Proyectos con Luis\proyecto3Simple\parking\parking\Views\Parking\SalidaVehicular.cshtml"
                                                                             Write(Html.DisplayNameFor(model => model.tiempoTotal));

#line default
#line hidden
#nullable disable
            WriteLiteral("</label>\r\n                    <div class=\"formulario__grupo-input\">\r\n                        <h6 class=\"text-center\"> ");
#nullable restore
#line 29 "D:\Proyectos con Luis\proyecto3Simple\parking\parking\Views\Parking\SalidaVehicular.cshtml"
                                            Write(Html.DisplayFor(model => model.tiempoTotal));

#line default
#line hidden
#nullable disable
            WriteLiteral(" </h6>\r\n                    </div>\r\n                </div>\r\n                <div class=\"form-group formulario__grupo\">\r\n                    <label class=\"control-label formulario__label text-center\">    ");
#nullable restore
#line 33 "D:\Proyectos con Luis\proyecto3Simple\parking\parking\Views\Parking\SalidaVehicular.cshtml"
                                                                              Write(Html.DisplayNameFor(model => model.MontoAPagar));

#line default
#line hidden
#nullable disable
            WriteLiteral("</label>\r\n                    <div class=\"formulario__grupo-input\">\r\n                        <h4 class=\"text-center text-danger font-weight-bold\">  ");
#nullable restore
#line 35 "D:\Proyectos con Luis\proyecto3Simple\parking\parking\Views\Parking\SalidaVehicular.cshtml"
                                                                          Write(Html.DisplayFor(model => model.MontoAPagar));

#line default
#line hidden
#nullable disable
            WriteLiteral(" S/. </h4>\r\n                    </div>\r\n                </div>\r\n                <div class=\"form-group formulario__grupo\">\r\n                    <label class=\"control-label formulario__label text-center\">   ");
#nullable restore
#line 39 "D:\Proyectos con Luis\proyecto3Simple\parking\parking\Views\Parking\SalidaVehicular.cshtml"
                                                                             Write(Html.DisplayNameFor(model => model.horasNocturnas));

#line default
#line hidden
#nullable disable
            WriteLiteral("</label>\r\n                    <div class=\"formulario__grupo-input\">\r\n                        <h6 class=\"text-center\">");
#nullable restore
#line 41 "D:\Proyectos con Luis\proyecto3Simple\parking\parking\Views\Parking\SalidaVehicular.cshtml"
                                           Write(Html.DisplayFor(model => model.horasNocturnas));

#line default
#line hidden
#nullable disable
            WriteLiteral(" </h6>\r\n                    </div>\r\n                </div>\r\n                <div class=\"form-group formulario__grupo\">\r\n                    <label class=\"control-label formulario__label text-center\">    ");
#nullable restore
#line 45 "D:\Proyectos con Luis\proyecto3Simple\parking\parking\Views\Parking\SalidaVehicular.cshtml"
                                                                              Write(Html.DisplayNameFor(model => model.horasDiurnas));

#line default
#line hidden
#nullable disable
            WriteLiteral("</label>\r\n                    <div class=\"formulario__grupo-input\">\r\n                        <h6 class=\"text-center\"> ");
#nullable restore
#line 47 "D:\Proyectos con Luis\proyecto3Simple\parking\parking\Views\Parking\SalidaVehicular.cshtml"
                                            Write(Html.DisplayFor(model => model.horasDiurnas));

#line default
#line hidden
#nullable disable
            WriteLiteral(" </h6>\r\n                    </div>\r\n                </div>\r\n                <div class=\"form-group ormulario__grupo formulario__grupo-btn-enviar\">\r\n\r\n                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "fedb523f8d8ae10ad3673126b1708f1ee41133be10419", async() => {
                WriteLiteral("\r\n                        <input");
                BeginWriteAttribute("value", " value=\"", 2265, "\"", 2318, 1);
#nullable restore
#line 53 "D:\Proyectos con Luis\proyecto3Simple\parking\parking\Views\Parking\SalidaVehicular.cshtml"
WriteAttributeValue(" ", 2273, Html.DisplayFor(model => model.tiempoTotal), 2274, 44, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" id=\"tiempoTotal\" name=\"tiempoTotal\" hidden />\r\n                        <input");
                BeginWriteAttribute("value", " value=\"", 2397, "\"", 2450, 1);
#nullable restore
#line 54 "D:\Proyectos con Luis\proyecto3Simple\parking\parking\Views\Parking\SalidaVehicular.cshtml"
WriteAttributeValue(" ", 2405, Html.DisplayFor(model => model.MontoAPagar), 2406, 44, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" id=\"MontoAPagar\" name=\"MontoAPagar\" hidden />\r\n                        <input");
                BeginWriteAttribute("value", " value=\"", 2529, "\"", 2585, 1);
#nullable restore
#line 55 "D:\Proyectos con Luis\proyecto3Simple\parking\parking\Views\Parking\SalidaVehicular.cshtml"
WriteAttributeValue(" ", 2537, Html.DisplayFor(model => model.horasNocturnas), 2538, 47, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" id=\"horasNocturnas\" name=\"horasNocturnas\" hidden />\r\n                        <input");
                BeginWriteAttribute("value", " value=\"", 2670, "\"", 2724, 1);
#nullable restore
#line 56 "D:\Proyectos con Luis\proyecto3Simple\parking\parking\Views\Parking\SalidaVehicular.cshtml"
WriteAttributeValue(" ", 2678, Html.DisplayFor(model => model.horasDiurnas), 2679, 45, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" id=\"horasDiurnas\" name=\"horasDiurnas\" hidden />\r\n                    ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "fedb523f8d8ae10ad3673126b1708f1ee41133be13723", async() => {
                WriteLiteral(" PDF ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n\r\n                </div>\r\n\r\n            </div>\r\n        </div>\r\n    </div>\r\n");
#nullable restore
#line 65 "D:\Proyectos con Luis\proyecto3Simple\parking\parking\Views\Parking\SalidaVehicular.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<hr />\r\n<hr />\r\n\r\n<div class=\"row\">\r\n    <div class=\"col-12\">\r\n        <section class=\"formularioHidden\">\r\n");
            WriteLiteral("            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "fedb523f8d8ae10ad3673126b1708f1ee41133be15716", async() => {
                WriteLiteral(@"
                <div class=""formulario__grupo-input form"" id=""formulario"">
                    <input type=""text"" class=""text-center"" name=""texto"" id=""texto"" />
                    <button id=""btnEnv"" class=""formulario__btn btn btn-info""> Enviar</button>
                </div>

            ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Controller = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_5.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
        </section>
    </div>
</div>


<script>

    const texto = document.getElementById(""texto"");
    const btnEnviar = document.getElementById(""btnEnv"");

    window.onload() = function() {
        texto.value = ""1=>xx/xx/xxx=>HH:mm""
        btnEnviar.click();
    }


</script>

");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<InfSalidaVehiculo> Html { get; private set; }
    }
}
#pragma warning restore 1591
