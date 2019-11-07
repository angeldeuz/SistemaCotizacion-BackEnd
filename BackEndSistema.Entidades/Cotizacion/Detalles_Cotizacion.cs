using System;
using System.Collections.Generic;
using System.Text;

namespace BackEndSistema.Entidades.Cotizacion
{
    public class Detalles_Cotizacion
    {
        public int idDetalleC { get; set; }
        public int idCotizacion { get; set; }
        public int idProducto { get; set; }
        public int cantidad { get; set; }
        public decimal monto { get; set; }
        public string comentarios { get; set; }
        public DateTime fechaRegistro { get; set; }
        public int idServicio { get; set; }
        public int idCategoria { get; set; }
       

        public CotizacionE cotizacione { get; set; }
        public Producto producto { get; set; }
    }
}
