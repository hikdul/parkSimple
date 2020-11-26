using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace parking.DataTransferObjects
{
    public class InfSalidaVehiculo
    {
        public int id { get; set; }
        public string tiempoTotal { get; set; }
        public int horasNocturnas { get; set; }
        public int horasDiurnas { get; set; }
        public int horasFinDeSemana { get; set; }
        public double MontoAPagar { get; set; }

    }
}
