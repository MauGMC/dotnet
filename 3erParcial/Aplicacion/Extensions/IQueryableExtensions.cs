using Aplicacion.Enums;
using Dominio.Entidades;

namespace Aplicacion.Extensions
{
    public static class IQueryableExtensions
    {
        public static IQueryable<Cliente> OrdenarPor(this IQueryable<Cliente> query, OrdenarClientePor ordenarPor, bool asc)
        {
            return ordenarPor switch
            {
                OrdenarClientePor.NombreCompleto => asc
                    ? query.OrderBy(c => c.Nombres + " " + c.ApellidoPaterno + " " + c.ApellidoMaterno)
                    : query.OrderByDescending(c => c.Nombres + " " + c.ApellidoPaterno + " " + c.ApellidoMaterno),

                OrdenarClientePor.Telefono => asc
                    ? query.OrderBy(c => c.Telefono)
                    : query.OrderByDescending(c => c.Telefono),

                OrdenarClientePor.FechaNacimiento => asc
                    ? query.OrderBy(c => c.FechaNacimiento)
                    : query.OrderByDescending(c => c.FechaNacimiento),

                OrdenarClientePor.Sexo => asc
                    ? query.OrderBy(c => c.Sexo)
                    : query.OrderByDescending(c => c.Sexo),

                _ => asc
                    ? query.OrderBy(c => c.ClienteID)
                    : query.OrderByDescending(c => c.ClienteID),
            };
        }
        public static IQueryable<Compra> OrdenarPor(this IQueryable<Compra> query, OrdenarCompraPor ordenarPor, bool asc)
        {
            return ordenarPor switch
            {
                OrdenarCompraPor.NumFactura => asc
                ? query.OrderBy(co => co.NumeroFactura)
                : query.OrderByDescending(co => co.NumeroFactura),
                OrdenarCompraPor.Empleado => asc
                ? query.OrderBy(co => co.EmpleadoID)
                : query.OrderByDescending(co => co.EmpleadoID),
                OrdenarCompraPor.Proveedor => asc
                ? query.OrderBy(co => co.ProveedorID)
                : query.OrderByDescending(co => co.ProveedorID),
                OrdenarCompraPor.Precio => asc
                ? query.OrderBy(co => co.Total)
                : query.OrderByDescending(co => co.Total),
                OrdenarCompraPor.FechaCompra => asc
                ? query.OrderBy(co=>co.FechaCompra)
                : query.OrderByDescending(co=>co.FechaCompra),
                OrdenarCompraPor.FechaEntrega => asc
                ? query.OrderBy(co=>co.FechaEntrega)
                : query.OrderByDescending(co=>co.FechaEntrega),
                OrdenarCompraPor.Estado => asc
                ? query.OrderBy(co=>co.Estado)
                : query.OrderByDescending(co=>co.Estado)
            };
        }
        public static IQueryable<Empleado> OrdenarPor(this IQueryable<Empleado> query, OrdenarEmpleadoPor ordenarPor, bool asc)
        {
            return ordenarPor switch
            {
                OrdenarEmpleadoPor.NombreCompleto => asc
                ? query.OrderBy(e => e.Nombres + e.ApellidoPaterno + e.ApellidoMaterno)
                : query.OrderByDescending(e => e.Nombres + e.ApellidoPaterno + e.ApellidoMaterno),
                OrdenarEmpleadoPor.Puesto => asc
                ? query.OrderBy(e => e.Puesto)
                : query.OrderByDescending(e => e.Puesto),
                OrdenarEmpleadoPor.FechaNacimiento => asc
                ? query.OrderBy(e=>e.FechaNacimiento)
                : query.OrderByDescending(e=>e.FechaNacimiento),
                OrdenarEmpleadoPor.Telefono => asc
                ? query.OrderBy(e=>e.Telefono)
                : query.OrderByDescending(e=>e.Telefono),
                OrdenarEmpleadoPor.Sexo => asc
                ? query.OrderBy(e=>e.Sexo)
                : query.OrderByDescending(e=>e.Sexo),
                OrdenarEmpleadoPor.Estado => asc
                ? query.OrderBy(e=>e.Estado)
                : query.OrderByDescending(e=>e.Estado)
            };
        }
        public static IQueryable<Pedido> OrdenarPor(this IQueryable<Pedido> query, OrdenarPedidoPor ordenarPor, bool asc)
        {
            return ordenarPor switch
            {
                OrdenarPedidoPor.Total => asc
                ? query.OrderBy(pe => pe.TotalConIVA)
                : query.OrderByDescending(pe => pe.TotalConIVA),
                OrdenarPedidoPor.FechaPedido => asc
                ? query.OrderBy(pe=>pe.FechaPedido)
                : query.OrderByDescending(pe=>pe.FechaPedido),
                OrdenarPedidoPor.FechaEntrega => asc
                ? query.OrderBy(pe=>pe.FechaEntrega)
                : query.OrderByDescending(pe=>pe.FechaEntrega),
                OrdenarPedidoPor.ClienteID => asc
                ? query.OrderBy(pe=>pe.ClienteID)
                : query.OrderByDescending(pe=>pe.ClienteID)
            };
        }
        public static IQueryable<ProductoEnCarrito> OrdenarPor(this IQueryable<ProductoEnCarrito> query, OrdenarProductosEnCarritoPor ordenarPor, bool asc)
        {
            return ordenarPor switch
            {
                OrdenarProductosEnCarritoPor.ProductoID => asc
                ? query.OrderBy(pc => pc.ProductoID)
                : query.OrderByDescending(pc => pc.ProductoID),
                OrdenarProductosEnCarritoPor.Precio => asc
                ? query.OrderBy(pc => pc.PrecioUnitario)
                : query.OrderByDescending(pc => pc.PrecioUnitario),
                OrdenarProductosEnCarritoPor.Nombre => asc
                ? query.OrderBy(pc => pc.Producto.Nombre)
                : query.OrderByDescending(pc => pc.Producto.Nombre),
                OrdenarProductosEnCarritoPor.ClienteID => asc
                ? query.OrderBy(pc => pc.Carrito.ClienteID)
                : query.OrderByDescending(pc => pc.Carrito.ClienteID),
                OrdenarProductosEnCarritoPor.CarritoID => asc
                ? query.OrderBy(pc => pc.CarritoID)
                : query.OrderByDescending(pc => pc.CarritoID),
                OrdenarProductosEnCarritoPor.Cantidad => asc
                ? query.OrderBy(pc => pc.Cantidad)
                : query.OrderByDescending(pc => pc.Cantidad)
            };
        }
        public static IQueryable<Producto> OrdenarPor(this IQueryable<Producto> query, OrdenarProductosPor ordenarPor, bool asc)
        {
            return ordenarPor switch
            {
                OrdenarProductosPor.Precio => asc
                ? query.OrderBy(p => p.Precio)
                : query.OrderByDescending(p => p.Precio),
                OrdenarProductosPor.Nombre => asc
                ? query.OrderBy(p => p.Nombre)
                : query.OrderByDescending(p => p.Nombre),
                OrdenarProductosPor.FechaCreacion => asc
                ? query.OrderBy(p => p.FechaCreacion)
                : query.OrderByDescending(p => p.FechaCreacion),
                OrdenarProductosPor.Categoria => asc
                ? query.OrderBy(p => p.Categoria)
                : query.OrderByDescending(p => p.Categoria),
                OrdenarProductosPor.Cantidad => asc
                ? query.OrderBy(p => p.Inventario.Cantidad)
                : query.OrderByDescending(p => p.Inventario.Cantidad),
                _ => asc
                ? query.OrderBy(p=>p.ProductoID) 
                : query.OrderByDescending(p=>p.ProductoID)
            };
        }
        public static IQueryable<Proveedor> OrdenarPor(this IQueryable<Proveedor> query, OrdenarProveedoresPor ordenarPor, bool asc)
        {
            return ordenarPor switch
            {
                OrdenarProveedoresPor.Telefono => asc
                ? query.OrderBy(p => p.Telefono)
                : query.OrderByDescending(p => p.Telefono),
                OrdenarProveedoresPor.Nombre => asc
                ? query.OrderBy(p => p.Nombre)
                : query.OrderByDescending(p => p.Nombre),
                OrdenarProveedoresPor.Email => asc
                ? query.OrderBy(p => p.Email)
                : query.OrderByDescending(p => p.Email),
                OrdenarProveedoresPor.Activo => asc
                ? query.OrderBy(p => p.Activo)
                : query.OrderByDescending(p => p.Activo)
            };
        }
        public static IQueryable<Usuario> OrdenarPor(this IQueryable<Usuario> query, OrdenarUsuariosPor ordenarPor, bool asc)
        {
            return ordenarPor switch
            {
                OrdenarUsuariosPor.Nombre => asc
                ? query.OrderBy(u=>u.NombreUsuario)
                : query.OrderByDescending(u=>u.NombreUsuario),
                OrdenarUsuariosPor.FechaCreacion => asc
                ? query.OrderBy(u=>u.FechaCreacion)
                : query.OrderByDescending(u=>u.FechaCreacion),
                OrdenarUsuariosPor.FechaAcceso => asc
                ? query.OrderBy(u=>u.UltimoAcceso)
                : query.OrderByDescending(u=>u.UltimoAcceso),
                OrdenarUsuariosPor.Activo => asc
                ? query.OrderBy(u=>u.Activo)
                : query.OrderByDescending(u=>u.Activo),
                OrdenarUsuariosPor.Correo => asc
                ? query.OrderBy(u=>u.Email)
                : query.OrderByDescending(u=> u.Email)
            };
        }
        public static IQueryable<Venta> OrdenarPor(this IQueryable<Venta> query, OrdenarVentasPor ordenarPor, bool asc)
        {
            return ordenarPor switch
            {
                OrdenarVentasPor.Total => asc
                ? query.OrderBy(v => v.TotalConIVA)
                : query.OrderByDescending(v => v.TotalConIVA),
                OrdenarVentasPor.MetodoPagoID => asc
                ? query.OrderBy(v => v.MetodoDePago.Tipo)
                : query.OrderByDescending(v => v.MetodoDePago.Tipo),
                OrdenarVentasPor.Estado => asc
                ? query.OrderBy(v => v.Estado)
                : query.OrderByDescending(v => v.Estado),
                OrdenarVentasPor.ClienteID => asc
                ? query.OrderBy(v => v.ClienteID)
                : query.OrderByDescending(v => v.ClienteID)
            };
        }
    }
}
