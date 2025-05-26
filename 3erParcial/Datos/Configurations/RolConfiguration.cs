namespace Datos.Configurations
{
    public class RolConfiguration : IEntityTypeConfiguration<Rol>
    {
        public void Configure(EntityTypeBuilder<Rol> builder)
        {
            //Tabla
            builder.ToTable("roles");
            //PK
            builder.HasKey(r => r.RolID);
            //Propiedades de navegación
            builder.HasMany(r => r.RolesDeUsuarios)
                .WithOne(u => u.Rol);
            builder.HasMany(r => r.PermisosRoles)
                .WithOne(p => p.Rol);
            //Atributos
            builder.Property(r => r.RolID)
                .HasColumnName("rol_id")
                .ValueGeneratedOnAdd();
            builder.Property(r => r.Nombre)
                .HasColumnName("nombre")
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(r => r.Descripcion)
                .HasColumnName("descripcion")
                .IsRequired()
                .HasMaxLength(200);
            builder.Property(r => r.Activo)
                .HasColumnName("activo")
                .IsRequired()
                .HasDefaultValue(true);
            builder.Property(r => r.FechaCreacion)
                .HasColumnName("fecha_creacion")
                .IsRequired();
        }
    }
}
