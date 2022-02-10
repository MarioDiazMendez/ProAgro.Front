using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProAgro.Front.Models
{
    public class CreateGeorrefenciaViewModels
    {
        public List<Estado> ListaEstados { get; set; }
        public Georreferencias Georreferencia { get; set; }
    }
}