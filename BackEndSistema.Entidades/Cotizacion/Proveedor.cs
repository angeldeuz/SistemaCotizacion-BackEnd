using System;
using System.Collections.Generic;
using System.Text;

namespace BackEndSistema.Entidades.Cotizacion
{
    public class Proveedor
    {
        public int idProveedor { get; set; }
        public string razonSocial { get; set; }
        public int diasCredito { get; set; }
        public string RFC { get; set; }
        public string correo { get; set; }
        public string nombreVendedor { get; set; }
    }
}
