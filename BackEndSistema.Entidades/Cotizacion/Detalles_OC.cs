using System;
using System.Collections.Generic;
using System.Text;

namespace BackEndSistema.Entidades.Cotizacion
{
    public class Detalles_OC
    {
        public int idDetalles_OC { get; set; }
        public int idProducto { get; set; }
        public int cantidad { get; set; }
        public int idOc { get; set; }
        public Producto productos { get; set; }
    }
}
