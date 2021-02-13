using EjemploCache.Usuarios.Dto;
using EjemploCache.Usuarios.Entity;
using EjemploCache.Usuarios.External.Empresa;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EjemploCache.Usuarios.Services.WithDistributedCache
{
    public interface IListUsersWithDistributedCache
    {
        Task<List<UsuarioEntity>> GetAllUsers();
        Task<EmpresaDto> GetEmpresa(int id);
    }

    public class ListUsersWithDistributedCache
    {
        private readonly IListUsersWithDistributedCache _dependencies;

        public ListUsersWithDistributedCache(IListUsersWithDistributedCache dependencies)
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
