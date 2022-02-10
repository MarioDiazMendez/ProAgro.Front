using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProAgro.Front.Models
{
    public class RespuestaAPI_Georreferencia
    {
        public List<Georreferencias> ListaGeorreferencias { get; set; }
        public string NombredelUsuario { get; set; }
        public string NombredelEstado { get; set; }
       
        public Respuesta_BackEnd RespuestaSP { get; set; }
    }
}