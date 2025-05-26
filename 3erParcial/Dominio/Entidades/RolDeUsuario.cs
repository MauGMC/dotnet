namespace Dominio.Entidades
{
    public class RolDeUsuario
    {
        //PK
        public int RolDeUsuarioID { get; set; }
        //FKs
        public string UsuarioID { get; set; }
        public int RolID { get; set; }
        //Propiedades de navegación EF
        public Usuario Usuario { get; set; } = new Usuario();
        public Rol Rol { get; set; } = new Rol();
    }
}
