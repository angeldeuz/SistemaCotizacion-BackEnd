using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndSistema.Web.Models.Cotizacion.OC
{
    public class DetalleViewModel
    {
        public int idProducto { get; set; }
        public int cantidad { get; set; }
        public int idOc {get; set;}
    }
}
