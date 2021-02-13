using EjemploCache.Usuarios.Entity;
using EjemploCache.Usuarios.External.Empresa;
using EjemploCache.Usuarios.External.Empresa.Services.Memory;
using EjemploCache.Usuarios.Repositories;
using EjemploCache.Usuarios.Services.WithCache;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EjemploCache.Usuarios.ServiceDependencies
{
    public class ListUsersMemoryCacheServiceDependencies : IListUsersWithMemoryCache
    {
        private readonly IEmpresaServicio _empersaServicio;
        private readonly IUsuarioRepository _userRepo;

        public ListUsersMemoryCacheServiceDependencies(IEmpresaServicio empersaServicio, IUsuarioRepository userRepo)
        {
            _empersaServicio = empersaServicio;
            _userRepo = userRepo;
        }

        public async Task<List<UsuarioEntity>> GetAllUsers()
        {
            return await _userRepo.GetAllUsers();
        }

        public async Task<EmpresaDto> GetEmpresa(int id)
        {
            return await _empersaServicio.GetEmpresa(id);
        }
    }
}
