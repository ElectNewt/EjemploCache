using EjemploCache.Usuarios.Dto;
using EjemploCache.Usuarios.Entity;
using EjemploCache.Usuarios.External.Empresa;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EjemploCache.Usuarios.Services.WithCache
{
    public interface IListUsersWithMemoryCache
    {
        Task<List<UsuarioEntity>> GetAllUsers();
        Task<EmpresaDto> GetEmpresa(int id);
    }

    public class ListUsersWithMemoryCache
    {
        private readonly IListUsersWithMemoryCache _dependencies;

        public ListUsersWithMemoryCache(IListUsersWithMemoryCache dependencies)
        {
            _dependencies = dependencies;
        }

        public async Task<List<UsuarioDto>> GetAllUsuarioDto()
        {
            List<UsuarioDto> resultUsuariosDto = new List<UsuarioDto>();

            List<UsuarioEntity> usuarios = await _dependencies.GetAllUsers();

            foreach (var usuario in usuarios)
            {
                EmpresaDto empresa = await _dependencies.GetEmpresa(usuario.IdEmpresa);

                UsuarioDto usuarioDto = new UsuarioDto
                {
                    Nombre = usuario.Nombre,
                    Apellido = usuario.Apellido,
                    NombreEmpresa = empresa.Nombre
                };
                resultUsuariosDto.Add(usuarioDto);
            }
            return resultUsuariosDto;
        }
    }
}
