using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndSistema.Web.Models.Cotizacion.CotizacionE
{
    public class ProyectoViewModel
    {
        [JsonProperty("proyecto")]
        public objProyecto objproyect { get; set; }
    }
}
