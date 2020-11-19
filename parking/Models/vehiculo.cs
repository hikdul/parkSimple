using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace parking.Models
{
    public class vehiculo
    {

        //public Image foto { get; set; }
        public int id { get; set; }

        public int costo { get; set; }   
        public bool activo { get; set; }     
        public string fechaI { get; set; }
        public string horaI { get; set; }
        public string fechaO { get; set; }
        public string horaO { get; set; }

       

    }
}
