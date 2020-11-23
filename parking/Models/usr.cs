using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace parking.Models
{
    public class usr
    {
        public int id { get; set; }
        [Required]
        public string nombre { get; set; }
        [Required]
        public string psw { get; set; }

        public int rol { get; set; }

        

    }
}
