using System;
using System.Collections.Generic;
using System.Text;

namespace BackEndSistema.Entidades.Cotizacion
{
    public class Categoria
    {
        public int idCategoria { get; set; }
        public string nombre { get; set; }
        public int idServicio { get; set; }
        public Servicio servicio { get; set; }
        public int idEstatus { get; set; }
        public Estatus estatus { get; set; }
        public DateTime fechaRegistro { get; set; }
    }
}
