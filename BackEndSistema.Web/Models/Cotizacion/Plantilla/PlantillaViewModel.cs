using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndSistema.Web.Models.Cotizacion.Plantilla
{
    public class PlantillaViewModel
    {
        public int idPlantilla { get; set; }
        public string nombre { get; set; }
        public string correo { get; set; }
        public int idEstatus { get; set; }
        public string estatus { get; set; }
        public DateTime fechaRegistro { get; set; }
    }
}
