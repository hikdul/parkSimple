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

        [Display(Name ="Nombre")]
        [Required]
        public string nombre { get; set; }

        [Required]
        [Display(Name ="Costo S/. hora")]
        [floatante]
        public double hora { get; set; }

        [floatante]
        [Required]
        [Display(Name = "Costo S/. Fraccion 30 min")]
        public double f30 { get; set; }

        [floatante]
        [Required]
        [Display(Name = "Costo S/. Fraccion 15 min")]
        public double f15 { get; set; }

        [floatante]
        [Required]
        [Display(Name = "Costo S/. Fraccion 5 min")]
        public double f5 { get; set; }

        [floatante]
        [Required]
        [Display(Name = "Costo S/. Hora Nocturna")]
        public double nocturno { get; set; }

        [floatante]
        [Required]
        [Display(Name = "Costo S/. Hora Fin De Semana")]
        public double weeekend { get; set; }

        public bool activo { get; set; }

    }
}
