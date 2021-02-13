namespace EjemploCache.Usuarios.Dto
{
    public record UsuarioDto
    {
        public string Nombre { get; init; }
        public string Apellido { get; init; }
        public string NombreEmpresa { get; init; }
    }
}
