namespace Infraestructura.Persistencia.Configurations
{
    public class MetodoPagoConfiguration : IEntityTypeConfiguration<MetodoDePago>
    {
        public void Configure(EntityTypeBuilder<MetodoDePago> builder)
        {
            //Tabla
            builder.ToTable("metodo_pago");
            //PK
            builder.HasKey(mp => mp.MetodoDePagoID);
            //FKs
            builder.HasOne(mp => mp.Cliente)
                .WithMany(c => c.MetodosDePago)
                .HasForeignKey(mp => mp.ClienteID)
                .OnDelete(DeleteBehavior.Restrict);
            //Atributos
            builder.Property(mp => mp.MetodoDePagoID)
                .HasColumnName("metodo_pago_id")
                .ValueGeneratedOnAdd();
            builder.Property(mp => mp.ClienteID)
                .HasColumnName("cliente_id")
                .IsRequired();
            builder.Property(mp => mp.Nombre)
                .HasColumnName("nombre")
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(mp => mp.NumeroTarjeta)
                .HasColumnName("numero_tarjeta")
                .IsRequired(false)
                .HasMaxLength(20);
            builder.Property(mp => mp.NombreTitular)
                .HasColumnName("nombre_titular")
                .IsRequired(false)
                .HasMaxLength(100);
            builder.Property(mp => mp.FechaExpiracion)
                .HasColumnName("fecha_expiracion")
                .IsRequired(false)
                .HasMaxLength(7);
            builder.Property(mp => mp.CVV)
                .HasColumnName("cvv")
                .IsRequired(false)
                .HasMaxLength(4);
            builder.Property(mp => mp.Tipo)
                .HasColumnName("tipo")
                .IsRequired(false)
                .HasMaxLength(50);
            builder.Property(mp => mp.Banco)
                .HasColumnName("banco")
                .IsRequired(false)
                .HasMaxLength(100);
            builder.Property(mp => mp.Referencia)
                .HasColumnName("referencia")
                .IsRequired(false)
                .HasMaxLength(200);
            builder.Property(mp => mp.Estado)
                .HasColumnName("estado")
                .IsRequired()
                .HasDefaultValue(1);
            builder.Property(mp => mp.Activo)
                .HasColumnName("activo")
                .IsRequired()
                .HasDefaultValue(true);
            //Indices
            builder.HasIndex(mp => mp.ClienteID);
        }
    }
}
