using System;
using System.Collections.Generic;
using System.Text;

namespace BackEndSistema.Entidades.Cotizacion
{
    public class OC
    {
        public int idOc { get; set; }
        public int idProveedor { get; set; }
        public decimal precio { get; set; }
        public int idCotizacion { get; set; }
        public DateTime fechaRegistro { get; set; }

        public Proveedor proveedores { get; set; }
        
    }
}
