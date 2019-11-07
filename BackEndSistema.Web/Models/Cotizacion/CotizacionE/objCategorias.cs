using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndSistema.Web.Models.Cotizacion.CotizacionE
{
    public class objCategorias
    {
        [JsonProperty("idCategoria")]
        public int idcategoria { get; set; }
        [JsonProperty("productos")]
        public List<objProductos> objproductos { get; set; }


        //datos que no guardo pero si llamo
        public string nombreCategoria { get; set; }

    }
}
