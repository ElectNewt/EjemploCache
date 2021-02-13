using EjemploCache.Usuarios.Entity;
using EjemploCache.Usuarios.External.Empresa;
using EjemploCache.Usuarios.External.Empresa.Services.Distributed;
using EjemploCache.Usuarios.Repositories;
using EjemploCache.Usuarios.Services.WithDistributedCache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EjemploCache.Usuarios.ServiceDependencies.WithDistributedCache
{

    public class ListUsersDistributedCacheServiceDependencies : IListUsersWithDistributedCache
    {
        private readonly IDistributedEmpresaServicio _empersaServicio;
        private readonly IUsuarioRepository _userRepo;

        public ListUsersDistributedCacheServiceDependencies(IDistributedEmpresaServicio empersaServicio, IUsuarioRepository userRepo)
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
