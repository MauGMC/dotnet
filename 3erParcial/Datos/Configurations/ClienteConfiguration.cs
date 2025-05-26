namespace Datos.Configurations
{
    public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            //Tabla
            builder.ToTable("clientes");
            //PK
            builder.HasKey(c => c.ClienteID);
            //FKs
            builder.HasOne(c => c.Usuario)
                .WithOne(u => u.Cliente)
                .HasForeignKey<Cliente>(c => c.UsuarioID);
            builder.HasMany(c => c.Direcciones)
                .WithOne(d => d.Cliente)
                .HasForeignKey(c => c.DireccionID);
            //Propiedades de navegación EF
            builder.HasOne(c => c.Carrito)
                .WithOne(carrito => carrito.Cliente);
            builder.HasMany(c => c.Pedidos)
               .WithOne(p => p.Cliente);
            builder.HasMany(c => c.Ventas)
                .WithOne(v => v.Cliente);
            builder.HasMany(c => c.MetodosDePago)
                .WithOne(mp => mp.Cliente);
            //Atributos
            builder.Property(c => c.Nombres)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(c => c.ApellidoPaterno)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(c => c.ApellidoMaterno)
                .HasMaxLength(50);
            builder.Property(c => c.Telefono)
                .IsRequired()
                .HasMaxLength(15)
                .HasMaxLength(100);
            builder.Property(c => c.FechaNacimiento)
                .IsRequired();
            builder.Property(c => c.Sexo)
                .HasColumnType("char(1)");
            //Indices
            builder.HasIndex(c => c.UsuarioID);
            builder.HasIndex(c => c.Telefono);

        }
    }
}
