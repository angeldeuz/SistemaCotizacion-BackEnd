﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndSistema.Web.Models.Cotizacion.Plantilla
{
    public class CrearViewModel
    {
        public string nombre { get; set; }
        public string correo { get; set; }
        public int idEstatus { get; set; }
        public DateTime fechaRegistro { get; set; }
    }
}
