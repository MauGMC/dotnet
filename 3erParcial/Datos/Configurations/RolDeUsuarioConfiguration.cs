namespace Datos.Configurations
{
    public class RolDeUsuarioConfiguration : IEntityTypeConfiguration<RolDeUsuario>
    {
        public void Configure(EntityTypeBuilder<RolDeUsuario> builder)
        {
            //Tabla
            builder.ToTable("roles_de_usuarios");
            //PK
            builder.HasKey(r => r.RolDeUsuarioID);
            //FKs
            builder.HasOne(r => r.Usuario)
                .WithMany(u => u.RolesDeUsuarios)
                .HasForeignKey(r => r.UsuarioID)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(r => r.Rol)
                .WithMany(ru => ru.RolesDeUsuarios)
                .HasForeignKey(r => r.RolID)
                .OnDelete(DeleteBehavior.Restrict);
            //Atributos
            builder.Property(r => r.RolDeUsuarioID)
                .HasColumnName("rol_de_usuario_id")
                .ValueGeneratedOnAdd();
            builder.Property(r => r.UsuarioID)
                .HasColumnName("usuario_id")
                .IsRequired()
                .HasMaxLength(50);
            //Indices
            builder.HasIndex(r => r.UsuarioID);
            builder.HasIndex(r => r.RolID);
        }
    }
}
