using System;
using System.Collections.Generic;
using System.Text;

namespace BackEndSistema.Entidades.Cotizacion
{
    public class Servicio
    {
        public int idServicio { get; set; }
        public string nombre { get; set; }
        public int idEstatus { get; set; }
        public DateTime fechaRegistro { get; set; }

        public Estatus estatus { get; set; }
    }
}
