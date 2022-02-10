using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProAgro.Front.Models
{
    public class RespuestaAPI_Permiso
    {
        public List<Permiso> ListaPermisos { get; set; }
        public List<EstadoUsuariosModel> EstadoUsuarioById { get; set; }
        public Respuesta_BackEnd RespuestaSP { get; set; }
    }
}