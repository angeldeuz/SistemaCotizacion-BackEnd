using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndSistema.Web.Models.Cotizacion.CotizacionE
{
    public class objServicios
    {
        [JsonProperty("idServicio")]
        public int idServicio { get; set; }

        [JsonProperty("categorias")]
        public List<objCategorias> objcategorias { get; set; }
        //public IEnumerable<objCategorias> categorias { get; set; }


        //datos de no guardo pero si mando llamar de la bd 
        public string nombreServicio { get; set; }
    }
}
