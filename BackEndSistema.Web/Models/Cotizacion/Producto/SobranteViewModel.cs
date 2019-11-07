using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndSistema.Web.Models.Cotizacion.Producto
{
    public class SobranteViewModel
    {
        public int idProducto { get; set; }
        public int stock { get; set; }
        public DateTime fechaActualizacion { get; set; }
    }
}
