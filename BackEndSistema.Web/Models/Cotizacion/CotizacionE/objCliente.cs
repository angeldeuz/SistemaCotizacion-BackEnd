using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndSistema.Web.Models.Cotizacion.CotizacionE
{
    public class objCliente
    {
        [JsonProperty("idCliente")]
        public int idCliente { get; set; }
        [JsonProperty("tipoCliente")]
        public string tipoCliente { get; set; }

        //Datos que no guardo pero si regreso
        public string nombre { get; set; }
        public string rfc { get; set; }
        public string razonSocial { get; set; }
        public int idTipoCliente { get; set; }
        public DateTime fechaRegistro { get; set; }
        public string correo { get; set; }
    }
}
