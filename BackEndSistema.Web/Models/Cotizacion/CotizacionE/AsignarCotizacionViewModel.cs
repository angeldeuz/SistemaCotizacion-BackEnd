using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndSistema.Web.Models.Cotizacion.CotizacionE
{
    public class AsignarCotizacionViewModel
    {
        public int idCotizacion { get; set; }
        public DateTime fechaActualizacion { get; set; }
        public int idEstatus { get; set; }
        public decimal horasPlaneadas { get; set; }
        public int idPlantilla { get; set; }

    }
}
