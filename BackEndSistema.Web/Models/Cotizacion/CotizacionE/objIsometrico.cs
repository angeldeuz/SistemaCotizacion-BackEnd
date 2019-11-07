using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndSistema.Web.Models.Cotizacion.CotizacionE
{
    public class objIsometrico
    {
        [JsonProperty("file")]
        public object file { get; set; }
    }
}
