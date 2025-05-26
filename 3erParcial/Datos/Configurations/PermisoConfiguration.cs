namespace Datos.Configurations
{
    public class PermisoConfiguration : IEntityTypeConfiguration<Permiso>
    {
        public void Configure(EntityTypeBuilder<Permiso> builder)
        {
            //Tabla
            builder.ToTable("permisos");
            //PK
            builder.HasKey(p => p.PermisoID);
            //Propiedad de navegación EF
            builder.HasMany(p => p.PermisoDeRoles)
                .WithOne(r => r.Permiso);
            //Atributos
            builder.Property(p => p.PermisoID)
                .HasColumnName("permiso_id")
                .ValueGeneratedOnAdd();
            builder.Property(p => p.Nombre)
                .HasColumnName("nombre")
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(p => p.Descripcion)
                .HasColumnName("descripcion")
                .IsRequired()
                .HasMaxLength(200);
        }
    }
}
