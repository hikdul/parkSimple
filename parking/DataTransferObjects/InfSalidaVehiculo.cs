using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace parking.DataTransferObjects
{
    public class InfSalidaVehiculo
    {

        public string tiempoTotal { get; set; }
        public int horasNocturnas { get; set; }
        public int horasDiurnas { get; set; }
        public double MontoAPagar { get; set; }

    }
}
