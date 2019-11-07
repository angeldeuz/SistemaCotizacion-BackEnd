﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndSistema.Web.Models.Cotizacion.Categoria
{
    public class CategoriaViewModel
    {
        public int idCategoria { get; set; }
        public string nombre { get; set; }
        public int idServicio { get; set; }
        public string servicio { get; set; }
        public int idEstatus { get; set; }
        public string estatus { get; set; }
        public DateTime fechaRegistro { get; set; }
    }
}
