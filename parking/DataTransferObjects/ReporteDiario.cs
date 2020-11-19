using parking.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace parking.DataTransferObjects
{
    public class ReporteDiario
    {
        [Display(Name = "Vehiculos Ingresados Hoy")]
        public int VehiculosIngresadosHoy { get; set; }
        [Display(Name = "Hitorico De Vehiculos")]
        public int totalVehiculos { get; set; }
        [Display(Name = "Vehiculos Que Salieron Hoy")]
        public int VehiculosQueHanSalido { get; set; }
        [Display(Name = "Vehiculos Que Se Encuentran Estacionados")]
        public int VehiculosQueNoHanSalido { get; set; }
        [Display(Name = "Dinero S/. Recojido Hoy")]
        public double cantidadDineroHoy { get; set; }
        [Display(Name = "Referencia De Costos")]
        public Costo costosUsados { get; set; }
    }
}
