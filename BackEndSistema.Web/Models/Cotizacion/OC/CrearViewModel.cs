using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndSistema.Web.Models.Cotizacion.OC
{
    public class CrearViewModel
    {
        public int idProveedor { get; set; }
        public decimal precio { get; set; }
        public int idCotizacion { get; set; }
        public DateTime fechaRegistro { get; set; }
        public List<DetalleViewModel> detalles { get; set; }
    }
}
