using BackEndSistema.Entidades.Cotizacion;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackEndSistema.Datos.Mapping.Cotizacion
{
    public class Detallles_OCMap : IEntityTypeConfiguration<Detalles_OC>
    {
        public void Configure(EntityTypeBuilder<Detalles_OC> builder)
        {
            builder.ToTable("Detalles_OC")
                .HasKey(d => d.idDetalles_OC);
        }
    }
}
