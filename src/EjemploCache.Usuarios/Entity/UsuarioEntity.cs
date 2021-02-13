namespace EjemploCache.Usuarios.Entity
{

    public record UsuarioEntity
    {
        public string Nombre { get; init; }
        public string Apellido { get; init; }
        public int IdEmpresa { get; init; }
    }
}
