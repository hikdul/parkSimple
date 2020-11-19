using parking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace parking.DataTransferObjects
{
    public class ReporteDiario
    {
        public int VehiculosIngresadosHoy { get; set; }
        public int totalVehiculos { get; set; }
        public int VehiculosQueHanSalido { get; set; }
        public int VehiculosQueNoHanSalido { get; set; }
        public double cantidadDineroHoy { get; set; }
        public Costo costosUsados { get; set; }
    }
}
