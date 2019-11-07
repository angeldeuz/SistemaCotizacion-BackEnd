using System;
using System.Collections.Generic;
using System.Text;

namespace BackEndSistema.Entidades.Cotizacion
{
    public class Producto
    {
        public int idProducto { get; set; }
        public string clave { get; set; }
        public string nombre { get; set; }
        public int idCategoria { get; set; }
        public Categoria categoria { get; set; }
        public string unidadMedida { get; set; }
        public int stock { get; set; }
        public decimal precioProducto { get; set; }
        public decimal manoObra { get; set; }
        public decimal precio { get; set; }
        public int idEstatus { get; set; }
        public Estatus estatus { get; set; }
        public DateTime fechaRegistro { get; set; }
        public DateTime fechaActualizacion { get; set; }
    }
}
