namespace EjemploCache.Empresa.Dto
{
    public record EmpresaDto
    {
        public int Id { get; init; }
        public string Nombre { get; init; }
        public string Ciudad { get; init; }
        public string Pais { get; init; }
    }
}
