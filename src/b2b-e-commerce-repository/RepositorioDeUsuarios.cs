using b2b_e_commerce_models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace b2b_e_commerce_repository
{
    public interface IRepositorioDeUsuarios
    {
        Task<Usuario> ObtenerUsuarioPorId(string id);
        Task<Usuario> ObtenerUsuarioPorCorreo(string correo);
    }

    public class RepositorioDeUsuarios : IRepositorioDeUsuarios
    {
        private readonly string COLLECTION_NAME = "usuarios";
        private readonly IMongoCollection<Usuario> _collection;
        public RepositorioDeUsuarios(IMongoClient client, IConfiguration configuracion)
        {
            ArgumentNullException.ThrowIfNull(client);

            var databaseName = configuracion["Mongo:Database"];

            if (string.IsNullOrWhiteSpace(databaseName))
            {
                throw new InvalidOperationException("MongoDB database name is not configured.");
            }

            _collection = client.GetDatabase(databaseName).GetCollection<Usuario>(COLLECTION_NAME);
        }

        public async Task<Usuario> ObtenerUsuarioPorId(string id)
        {
            var filter = Builders<Usuario>.Filter.Eq(u => u.Id, id);

            var usuario = await _collection.Find(filter).FirstOrDefaultAsync();

            return usuario;
        }

        public async Task<Usuario> ObtenerUsuarioPorCorreo(string correo)
        {
            var filter = Builders<Usuario>.Filter.Eq(u => u.Correo, correo);

            var usuario = await _collection.Find(filter).FirstOrDefaultAsync();

            return usuario;
        }
    }
}