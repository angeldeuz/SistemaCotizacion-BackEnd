using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndSistema.Web.Models.Cotizacion.CotizacionE
{
    public class objProductos
    {
        [JsonProperty("idProducto")]
        public int idProducto { get; set; }
        [JsonProperty("cantidad")]
        public string cantidad { get; set; }
        [JsonProperty("precioCotizacion")]
        public string monto { get; set; }



        //datos que no guardo pero si mando llamar
        public string clave { get; set; }
        public string unidadMedida { get; set; }
        public int stock { get; set; }
        public decimal precioProducto { get; set; }
        public decimal manoObra { get; set; }
        public decimal precio { get; set; }


    }
}
