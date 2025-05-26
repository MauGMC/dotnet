namespace Infraestructura.Persistencia.Configurations
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
            builder.OwnsOne(u => u.Email, email =>
            {
                email.Property(e => e.Valor)
                    .HasColumnName("email")
                    .HasMaxLength(100)
                    .IsRequired();
            });
            builder.Property(u => u.Contrasena)
                .HasColumnName("contrasena")
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
            builder.Property(u => u.RutaImagenPerfil)
                .HasColumnName("ruta_imagen_perfil")
                .IsRequired(false)
                .HasMaxLength(200);
        }
    }
}
