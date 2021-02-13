using Microsoft.Extensions.Caching.Memory;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace EjemploCache.Usuarios.External.Empresa.Services.Memory
{
    public interface IEmpresaServicio
    {
        Task<EmpresaDto> GetEmpresa(int id);
    }

    public class EmpresaServicio : IEmpresaServicio
    {
        private readonly MemoryCache _cache;
        private readonly IHttpClientFactory _httpClientFactory;

        public EmpresaServicio(IHttpClientFactory httpClientFactory)
        {
            _cache = new MemoryCache(new MemoryCacheOptions());
            _httpClientFactory = httpClientFactory;
        }

        public async Task<EmpresaDto> GetEmpresa(int id)
        {
            //Comprobar si existe
            if(!_cache.TryGetValue(id, out EmpresaDto empresa))
            {
                //Conslutar el elemenot en el microservicio
                empresa = await GetFromMicroservicio(id);
                _cache.Set(id, empresa);
                return empresa;
            }

            return empresa;
        }

        private async Task<EmpresaDto> GetFromMicroservicio(int id)
        {
            HttpClient client = _httpClientFactory.CreateClient("EmpresaMS");
            return await client.GetFromJsonAsync<EmpresaDto>($"empresa/{id}");
        }
    }
}
