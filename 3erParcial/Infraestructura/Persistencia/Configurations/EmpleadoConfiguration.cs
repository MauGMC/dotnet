namespace Infraestructura.Persistencia.Configurations
{
    public class EmpleadoConfiguration : IEntityTypeConfiguration<Empleado>
    {
        public void Configure(EntityTypeBuilder<Empleado> builder)
        {
            //Tabla
            builder.ToTable("empleados");
            //PK
            builder.HasKey(e => e.EmpleadoID);
            //FKs
            builder.HasOne(e => e.Usuario)
                .WithOne(u => u.Empleado)
                .HasForeignKey<Empleado>(e => e.UsuarioID)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(e => e.Direccion)
                .WithMany(d => d.Empleados)
                .HasForeignKey(e => e.DireccionID)
                .OnDelete(DeleteBehavior.Restrict);
            //Propiedades de navegación EF
            builder.HasMany(e => e.Compras)
                .WithOne(c => c.Empleado);
            //Atributos
            builder.Property(e => e.EmpleadoID)
                .HasColumnName("empleado_id")
                .ValueGeneratedOnAdd();
            builder.Property(e => e.UsuarioID)
                .HasColumnName("usuario_id")
                .IsRequired();
            builder.Property(e => e.DireccionID)
                .HasColumnName("direccion_id")
                .IsRequired();
            builder.Property(e => e.Nombres)
                .HasColumnName("nombres")
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(e => e.ApellidoPaterno)
                .HasColumnName("apellido_paterno")
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(e => e.ApellidoMaterno)
                .HasColumnName("apellido_materno")
                .IsRequired(false)
                .HasMaxLength(50);
            builder.Property(e => e.Telefono)
                .HasColumnName("telefono")
                .IsRequired()
                .HasMaxLength(15);
            builder.Property(e => e.FechaNacimiento)
                .HasColumnName("fecha_nacimiento")
                .IsRequired();
            builder.Property(e => e.Sexo)
                .HasColumnName("sexo")
                .IsRequired(false);
            builder.Property(e => e.Puesto)
                .HasColumnName("puesto")
                .IsRequired(false)
                .HasMaxLength(50);
            builder.Property(e => e.Estado)
                .HasColumnName("estado")
                .HasDefaultValue(1)
                .IsRequired();
            builder.Property(e => e.FechaContratacion)
                .HasColumnName("fecha_contratacion")
                .IsRequired(false);
            //Indices
            builder.HasIndex(e => e.UsuarioID);
            builder.HasIndex(e => e.DireccionID);
            builder.HasIndex(e => e.Telefono);
        }
    }
}
