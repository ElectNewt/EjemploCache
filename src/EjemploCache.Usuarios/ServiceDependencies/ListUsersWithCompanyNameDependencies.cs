using EjemploCache.Usuarios.Entity;
using EjemploCache.Usuarios.External.Empresa;
using EjemploCache.Usuarios.Repositories;
using EjemploCache.Usuarios.Services;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace EjemploCache.Usuarios.ServiceDependencies
{
    public class ListUsersWithCompanyNameDependencies : IListUsersWithCompanyNameDependencies
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IUsuarioRepository _userRepo;

        public ListUsersWithCompanyNameDependencies(IHttpClientFactory httpClientFactory, IUsuarioRepository userRepo)
        {
            _httpClientFactory = httpClientFactory;
            _userRepo = userRepo;
        }

        public async Task<List<UsuarioEntity>> GetAllUsers()
        {
            return await _userRepo.GetAllUsers();
        }

        public async Task<EmpresaDto> GetEmpresa(int id)
        {
            HttpClient client = _httpClientFactory.CreateClient("EmpresaMS");
            return await client.GetFromJsonAsync<EmpresaDto>($"empresa/{id}");
        }
    }
}
