using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProAgro.Front.Models
{
    public class Georreferencias
    {
        public int idGeorreferencia { get; set; }

        public int? idEstado { get; set; }

       
        public string Latitud { get; set; }

       
        public string Longitud { get; set; }
    }
}