using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProAgro.Front.Models
{
    public class EstadoUsuariosModel
    {
        public int idEstado { get; set; }
        public string NombreEstado { get; set; }
        public int idUsuario { get; set; }
        public string NombreUsuario { get; set; }
    }
}