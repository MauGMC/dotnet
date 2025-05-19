using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructura.DbConfigurations
{
    public class CsvLogConfiguration : IEntityTypeConfiguration<CsvLog>
    {
        public void Configure(EntityTypeBuilder<CsvLog> builder)
        {
            //Tabla
            builder.ToTable("CsvLogs")
                    .HasKey(c=>c.CsvLogID);

            //Propiedades
            builder.Property(c => c.CsvLogID)
                .HasColumnName("CsvLogID")
                .HasColumnType("int unsigned")
                .IsRequired()
                .ValueGeneratedOnAdd();
            builder.Property(c => c.Archivo)
                .HasColumnName("Archivo")
                .HasColumnType("longblob")
                .IsRequired();

            builder.Property(c => c.NombreArchivo)
                .HasColumnName("NombreArchivo")
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder.Property(c => c.FechaImportacion)
                .HasColumnName("FechaImportacion")
                .HasColumnType("datetime")
                .IsRequired();

            builder.Property(c => c.ProductosImportados)
                .HasColumnName("NumeroExitos")
                .HasColumnType("int unsigned")
                .IsRequired();

            builder.Property(c => c.ProductosConAdvertencias)
                .HasColumnName("NumeroAdvertencias")
                .HasColumnType("int unsigned")
                .IsRequired();

            builder.Property(c => c.ProductosConErrores)
                .HasColumnName("NumeroErrores")
                .HasColumnType("int unsigned")
                .IsRequired();

            builder.Property(c => c.Usuario)
                .HasColumnName("Usuario")
                .HasColumnType("varchar(100)")
                .IsRequired(false);

            builder.Property(c => c.Observaciones)
                .HasColumnName("Observaciones")
                .HasColumnType("varchar(255)")
                .IsRequired(false);
        }
    }
}
