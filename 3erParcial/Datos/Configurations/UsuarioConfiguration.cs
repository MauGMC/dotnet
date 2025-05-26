namespace Datos.Configurations
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            //Tabla
            builder.ToTable("usuario");
            //PK
            builder.HasKey(u => u.UsuarioID);
            //Propiedades de navegación
            builder.HasMany(u => u.RolesDeUsuarios)
                .WithOne(ru => ru.Usuario);
            builder.HasMany(u => u.NotificacionesPorUsuarios)
                .WithOne(n => n.Usuario);
            builder.HasOne(u => u.Empleado)
                .WithOne(e => e.Usuario);
            builder.HasOne(u => u.Cliente)
                .WithOne(c => c.Usuario);
            //Atributos
            builder.Property(u => u.UsuarioID)
                .HasColumnName("usuario_id")
                .ValueGeneratedOnAdd();
            builder.Property(u => u.NombreUsuario)
                .HasColumnName("nombre_usuario")
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(u => u.Contrasena)
                .HasColumnName("contrasena")
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(u => u.Correo)
                .HasColumnName("correo_electronico")
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(u => u.FechaCreacion)
                .HasColumnName("fecha_creacion")
                .IsRequired();
            builder.Property(u => u.UltimoAcceso)
                .HasColumnName("ultimo_acceso")
                .IsRequired(false);
            builder.Property(u => u.Activo)
                .HasColumnName("activo")
                .IsRequired();
        }
    }
}
