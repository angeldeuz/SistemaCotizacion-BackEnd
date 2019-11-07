using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndSistema.Web.Models.Cotizacion.Categoria
{
    public class ActualizarViewModel
    {
        public int idCategoria { get; set; }
        public string nombre { get; set; }
        public int idServicio { get; set; }
    }
}
