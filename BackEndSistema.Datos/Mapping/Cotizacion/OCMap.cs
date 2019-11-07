using BackEndSistema.Entidades.Cotizacion;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackEndSistema.Datos.Mapping.Cotizacion
{
    public class OCMap : IEntityTypeConfiguration<OC>
    {
        public void Configure(EntityTypeBuilder<OC> builder)
        {
            builder.ToTable("Orden_Compra")
                .HasKey(o => o.idOc);
        }
    }
}
