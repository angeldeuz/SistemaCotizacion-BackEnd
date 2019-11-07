using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndSistema.Web.Models.Cotizacion.CotizacionE
{
    public class FinalizarCotizacionViewModel
    {
        public int idCotizacion { get; set; }
        public DateTime fechaActualizacion { get; set; }
        public int idEstatus { get; set; }
        public decimal horasFinales { get; set; }
    }
}
