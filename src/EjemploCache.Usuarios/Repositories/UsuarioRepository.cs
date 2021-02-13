using EjemploCache.Usuarios.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EjemploCache.Usuarios.Repositories
{

    public interface IUsuarioRepository
    {
        Task<List<UsuarioEntity>> GetAllUsers();
    }

    public class UsuarioRepository : IUsuarioRepository
    {

        public Task<List<UsuarioEntity>> GetAllUsers()
        {
            List<UsuarioEntity> allUsers = new List<UsuarioEntity>();

            for (int i = 1; i <= 3; i++)
                for (int k = 1; k <= 10; k++)
                {
                    UsuarioEntity user = new UsuarioEntity
                    {
                        Apellido = $"apellido{k}",
                        Nombre = $"Nombre{k}",
                        IdEmpresa = i
                    };
                    allUsers.Add(user);
                }

            return Task.FromResult(allUsers);
        }

    }
}
