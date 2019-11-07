using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndSistema.Web.Models.Cotizacion.CotizacionE
{
    public class CotizacionViewModel
    {
        public int idCotizacion { get; set; }
        public int idUsuario { get; set; }
        public string usuario { get; set; }
        public DateTime fechaRegistro { get; set; }
        public int idCliente { get; set; }
        public string nombre { get; set; }
        public int idEstatus { get; set; }
        public string estatus { get; set; }
        public string moneda { get; set; }
        public decimal tipoCambio { get; set; }
        public decimal total { get; set; }
        public decimal descuento { get; set; }
        public decimal aumento { get; set; }
        public decimal horasPlaneadas { get; set; }
        public decimal horasFinales { get; set; }

        public int idPlantilla { get; set; }
        public string nombreP { get; set; }

        public IEnumerable<objCliente> clientes { get; set; }
        public object DatosCotizacion { get; set; }
        //public List<objServicios> servicios { get; set; }
        //public List<objCategorias> categorias { get; set; }

        //public IEnumerable<objCategorias> categorias { get; set; }
    }
}
