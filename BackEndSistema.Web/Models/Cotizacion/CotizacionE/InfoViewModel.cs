using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndSistema.Web.Models.Cotizacion.CotizacionE
{
    public class InfoViewModel
    {
        public List<objServicios> servicios { get; set; }
        public List<objCategorias> categorias { get; set; }
        public List<objProductos> productos { get; set; }
    }
}
