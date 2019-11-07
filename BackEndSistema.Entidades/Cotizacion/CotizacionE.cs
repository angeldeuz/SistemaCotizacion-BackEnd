using BackEndSistema.Entidades.Usuarios;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackEndSistema.Entidades.Cotizacion
{
    public class CotizacionE
    {
        public int idCotizacion { get; set; }
        public int idUsuario { get; set; }
        public DateTime fechaRegistro { get; set; }
        public DateTime fechaActualizacion { get; set; }
        public int idCliente { get; set; }
        public int idEstatus { get; set; }
        public string moneda { get; set; }
        public decimal tipoCambio { get; set; }
        public decimal total { get; set; }
        public decimal descuento { get; set; }
        public decimal aumento { get; set; }

        public decimal horasPlaneadas { get; set; }
        public decimal horasFinales { get; set; }

        public int idPlantilla { get; set; }

        public ICollection<Detalles_Cotizacion> detalles { get; set; }
        public Usuario usuarios { get; set; }
        public Cliente cliente { get; set; }
        public Estatus estatus { get; set; }
        public Plantilla plantillas { get; set; }

    }
}
