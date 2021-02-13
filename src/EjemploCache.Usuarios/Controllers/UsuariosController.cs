using EjemploCache.Usuarios.Dto;
using EjemploCache.Usuarios.Services;
using EjemploCache.Usuarios.Services.WithCache;
using EjemploCache.Usuarios.Services.WithDistributedCache;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EjemploCache.Usuarios.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly ListUsersWithCompanyName _usersWithNameCompany;
        private readonly ListUsersWithMemoryCache _usersWithMemoryCache;
        private readonly ListUsersWithDistributedCache _usersWithDistributedCache;
        public UsuariosController(ListUsersWithCompanyName usersWithNameCompany, ListUsersWithMemoryCache usersWithMemoryCache,
            ListUsersWithDistributedCache usersWithDistributedCache)
        {
            _usersWithNameCompany = usersWithNameCompany;
            _usersWithMemoryCache = usersWithMemoryCache;
            _usersWithDistributedCache = usersWithDistributedCache;
        }

        [HttpGet]
        public Task<List<UsuarioDto>> GetAllUsers()
        {
            return _usersWithNameCompany.GetAllUsuarioDto();
        }

        [HttpGet("ejemploMemoryCache")]
        public Task<List<UsuarioDto>> GetAllUsersMemoryCache()
        {
            return _usersWithMemoryCache.GetAllUsuarioDto();
        }

        [HttpGet("ejemploDistributedCache")]
        public Task<List<UsuarioDto>> GetAllUsersDistributedCache()
        {
            return _usersWithDistributedCache.GetAllUsuarioDto();
        }
    }
}
