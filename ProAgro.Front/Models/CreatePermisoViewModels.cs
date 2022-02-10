using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProAgro.Front.Models
{
    public class CreatePermisoViewModels
    {
        public List<Estado> ListaEstados { get; set; }
        public List<Usuario> ListaUsuarios { get; set; }
        public Permiso permiso { get; set; }
    }
}