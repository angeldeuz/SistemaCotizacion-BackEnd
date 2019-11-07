using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndSistema.Web.Models.Cotizacion.CotizacionE
{
    public class objMoneda
    {
        [JsonProperty("moneda")]
        public string moneda { get; set; }
        [JsonProperty("valor")]
        public decimal tipoCambio { get; set; }

    }
}
