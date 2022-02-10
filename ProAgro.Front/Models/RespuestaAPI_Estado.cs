using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProAgro.Front.Models
{
    public class RespuestaAPI_Estado
    {
        public List<Estado> ListaEstados { get; set; }
        public Respuesta_BackEnd RespuestaSP { get; set; }
    }
}