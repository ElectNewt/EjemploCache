using Microsoft.Extensions.Caching.Distributed;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace EjemploCache.Usuarios.External.Empresa.Services.Distributed
{

    public interface IDistributedEmpresaServicio
    {
        Task<EmpresaDto> GetEmpresa(int id);
    }

    public class EmpresaServicio : IDistributedEmpresaServicio
    {
        private readonly IDistributedCache _cache;
        private readonly IHttpClientFactory _httpClientFactory;

        public EmpresaServicio(IHttpClientFactory httpClientFactory, IDistributedCache cache)
        {
            _cache = cache;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<EmpresaDto> GetEmpresa(int id)
        {
            byte[] value = await _cache.GetAsync(id.ToString());
            if (value == null)
            {
                EmpresaDto empresaDto = await GetFromMicroservicio(id);

                if (empresaDto != null)
                    await AddToCache(empresaDto);

                return empresaDto;
            }

            return FromByteArray(value);

        }

        private async Task<EmpresaDto> GetFromMicroservicio(int id)
        {
            HttpClient client = _httpClientFactory.CreateClient("EmpresaMS");
            return await client.GetFromJsonAsync<EmpresaDto>($"empresa/{id}");
        }

        private async Task AddToCache(EmpresaDto empesa)
        {
            await _cache.SetAsync(empesa.Id.ToString(), ToByteArray(empesa));
        }

        private byte[] ToByteArray(EmpresaDto obj)
        {
            return JsonSerializer.SerializeToUtf8Bytes(obj);
        }

        private EmpresaDto FromByteArray(byte[] data)
        {
            return JsonSerializer.Deserialize<EmpresaDto>(data);
        }
    }
}
