using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndSistema.Web.Models.Cotizacion.Proveedor
{
    public class ProveedorViewModel
    {
        public int idProveedor { get; set; }
        public string razonSocial { get; set; }
        public int diasCredito { get; set; }
        public string RFC { get; set; }
        public string correo { get; set; }
        public string nombreVendedor { get; set; }
    }
}
