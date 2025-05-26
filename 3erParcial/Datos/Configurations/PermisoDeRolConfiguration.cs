namespace Datos.Configurations
{
    public class PermisoDeRolConfiguration : IEntityTypeConfiguration<PermisoDeRol>
    {
        public void Configure(EntityTypeBuilder<PermisoDeRol> builder)
        {
            //Tabla
            builder.ToTable("permisos_de_roles");
            //PK
            builder.HasKey(pr => pr.PermisoDeRolID);
            //FKs
            builder.HasOne(pr => pr.Rol)
                .WithMany(r => r.PermisosRoles)
                .HasForeignKey(pr => pr.RolID)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(pr => pr.Permiso)
                .WithMany(p => p.PermisoDeRoles)
                .HasForeignKey(pr => pr.PermisoID)
                .OnDelete(DeleteBehavior.Restrict);
            //Atributos
            builder.Property(pr => pr.PermisoDeRolID)
                .HasColumnName("permiso_de_rol_id")
                .ValueGeneratedOnAdd();
            builder.Property(pr => pr.RolID)
                .HasColumnName("rol_id")
                .IsRequired();
            builder.Property(pr => pr.PermisoID)
                .HasColumnName("permiso_id")
                .IsRequired();
            //Indices
            builder.HasIndex(pr => pr.RolID);
            builder.HasIndex(pr => pr.PermisoID);
        }
    }
}
