using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProAgro.Front.Models
{
    public class Usuario
    {
       
        public int idUsuario { get; set; }
      
        public string Contrasena { get; set; }
     
        public string Nombre { get; set; }

        public DateTime? FechaNacimiento { get; set; }

        public DateTime? FechaCreacion { get; set; }
      
        public string RFC { get; set; }
    }
}