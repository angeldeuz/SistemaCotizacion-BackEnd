using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndSistema.Web.Models.Cotizacion.CotizacionE
{
    public class objProyectoActualizar
    {
        [JsonProperty("isometrico")]
        public objIsometrico objisometrico { get; set; }
        [JsonProperty("cliente")]
        public objCliente objcliente { get; set; }
        [JsonProperty("moneda")]
        public objMoneda objmoneda { get; set; }
        [JsonProperty("servicios")]
        public List<objServicios> objservicios { get; set; }
        [JsonProperty("descuento")]
        public decimal descuento { get; set; }
        [JsonProperty("totalProyecto")]
        public decimal totalProyecto { get; set; }
        [JsonProperty("idCotizacion")]
        public int idCotizacion { get; set; }

    }
}

