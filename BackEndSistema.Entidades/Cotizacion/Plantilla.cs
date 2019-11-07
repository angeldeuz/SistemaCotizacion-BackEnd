using System;
using System.Collections.Generic;
using System.Text;

namespace BackEndSistema.Entidades.Cotizacion
{
    public class Plantilla
    {
        public int idPlantilla { get; set; }
        public string nombre { get; set; }
        public string correo { get; set; }
        public int idEstatus { get; set; }
        public Estatus estatus { get; set; }
        public DateTime fechaRegistro { get; set; }
    }
}
