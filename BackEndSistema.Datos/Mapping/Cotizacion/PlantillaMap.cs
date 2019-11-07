using BackEndSistema.Entidades.Cotizacion;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackEndSistema.Datos.Mapping.Cotizacion
{
    public class PlantillaMap : IEntityTypeConfiguration<Plantilla>
    {
        public void Configure(EntityTypeBuilder<Plantilla> builder)
        {
            builder.ToTable("Plantillas")
                .HasKey(p => p.idPlantilla);
        }
    }
}
