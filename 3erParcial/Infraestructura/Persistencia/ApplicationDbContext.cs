global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Metadata.Builders;
global using Dominio.Entidades;
using Infraestructura.Persistencia.Configurations;    
namespace Infraestructura.Persistencia
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options){ }

        public DbSet<Carrito> Carritos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Compra> Compras { get; set; }
        public DbSet<DetalleCompra> DetallesCompras { get; set; }
        public DbSet<DetallePedido> DetallesPedidos { get; set; }
        public DbSet<DetalleVenta> DetallesVentas { get; set; }
        public DbSet<Direccion> Direcciones { get; set; }
        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<Imagen> Imagenes { get; set; }
        public DbSet<Inventario> Inventarios { get; set; }
        public DbSet<MetodoDePago> MetodosDePago { get; set; }
        public DbSet<Notificacion> Notificaciones { get; set; }
        public DbSet<NotificacionPorUsuario> NotificacionesPorUsuarios { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Permiso> Permisos { get; set; }
        public DbSet<PermisoDeRol> PermisosRoles { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<ProductoEnCarrito> ProductosEnCarrito { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<RolDeUsuario> RolesUsuarios { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Venta> Ventas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CarritoConfiguration());
            modelBuilder.ApplyConfiguration(new ClienteConfiguration());
            modelBuilder.ApplyConfiguration(new CompraConfiguration());
            modelBuilder.ApplyConfiguration(new DetalleCompraConfiguration());
            modelBuilder.ApplyConfiguration(new DetallePedidoConfiguration());
            modelBuilder.ApplyConfiguration(new DetalleVentaConfiguration());
            modelBuilder.ApplyConfiguration(new DireccionConfiguration());
            modelBuilder.ApplyConfiguration(new EmpleadoConfiguration());
            modelBuilder.ApplyConfiguration(new ImagenConfiguration());
            modelBuilder.ApplyConfiguration(new InventarioConfiguration());
            modelBuilder.ApplyConfiguration(new MetodoPagoConfiguration());
            modelBuilder.ApplyConfiguration(new NotificacionConfiguration());
            modelBuilder.ApplyConfiguration(new NotificacionUsuarioConfiguration());
            modelBuilder.ApplyConfiguration(new PedidoConfiguration());
            modelBuilder.ApplyConfiguration(new PermisoConfiguration());
            modelBuilder.ApplyConfiguration(new PermisoDeRolConfiguration());
            modelBuilder.ApplyConfiguration(new ProductoConfiguration());
            modelBuilder.ApplyConfiguration(new ProductoEnCarritoConfiguration());
            modelBuilder.ApplyConfiguration(new ProveedorConfiguration());
            modelBuilder.ApplyConfiguration(new RolConfiguration());
            modelBuilder.ApplyConfiguration(new RolDeUsuarioConfiguration());
            modelBuilder.ApplyConfiguration(new UsuarioConfiguration());
            modelBuilder.ApplyConfiguration(new VentaConfiguration());
            base.OnModelCreating(modelBuilder);
        }


    }
}
