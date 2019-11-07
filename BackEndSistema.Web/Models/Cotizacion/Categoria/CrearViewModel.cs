using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndSistema.Web.Models.Cotizacion.Categoria
{
    public class CrearViewModel
    {
        public string nombre { get; set; }
        public int idServicio { get; set; }
        public int idEstatus { get; set; }
        public DateTime fechaRegistro { get; set; }
    }
}
