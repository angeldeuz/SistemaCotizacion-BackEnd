using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndSistema.Web.Models.Cotizacion.OC
{
    public class OCViewModel
    {
        public int idOc { get; set; }
        public int idProveedor { get; set; }
        public decimal precio { get; set; }
        public int idCotizacion { get; set; }
        public DateTime fechaRegistro { get; set; }

        public string proveedor { get; set; }
    }
}
