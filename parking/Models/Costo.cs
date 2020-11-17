using parking.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace parking.Models
{
    public class Costo
    {
        public int id { get; set; }
        [Required]
        public string nombre { get; set; }
        [Required]
        [floatante]
        public double hora { get; set; }
        [floatante]
        [Required]

        public double f30 { get; set; }
        [floatante]
        [Required]

        public double f15 { get; set; }
        [floatante]
        [Required]

        public double f5 { get; set; }
        [floatante]
        [Required]

        public double nocturno { get; set; }

        public bool activo { get; set; }

    }
}
