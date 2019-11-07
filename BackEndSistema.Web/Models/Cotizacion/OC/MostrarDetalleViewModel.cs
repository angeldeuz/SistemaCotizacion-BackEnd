using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndSistema.Web.Models.Cotizacion.OC
{
    public class MostrarDetalleViewModel
    {
        public int idDetalles_OC {get; set;}
        public int idProducto { get; set; }
        public int cantidad { get; set; }
        public int idOc { get; set; }
        public string producto { get; set; }
    }
}
