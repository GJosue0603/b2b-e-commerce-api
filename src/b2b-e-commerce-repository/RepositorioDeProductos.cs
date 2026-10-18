
using b2b_e_commerce_models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace b2b_e_commerce_repository
{
    public interface IRepositorioDeProductos
    {
        Task<List<Producto>> ObtenerProductos();
        Task<Producto> ObtenerProductoPorId(string id);
    }

    public class RepositorioDeProductos : IRepositorioDeProductos
    {
        private readonly string COLLECTION_NAME = "productos";
        private readonly IMongoCollection<Producto> _collection;

        public RepositorioDeProductos(IMongoClient client,IConfiguration configuracion)
        {
            ArgumentNullException.ThrowIfNull(client);
            var databaseName = configuracion["Mongo:Database"];

            if (string.IsNullOrWhiteSpace(databaseName))
            {
                throw new InvalidOperationException("MongoDB database name is not configured.");
            }

            _collection = client.GetDatabase(databaseName).GetCollection<Producto>(COLLECTION_NAME);
        }

        public async Task<List<Producto>> ObtenerProductos()
        {
            var filter = Builders<Producto>.Filter.Empty;

            var productos = await _collection.Find(filter).ToListAsync();

            return productos;
        }

        public async Task<Producto> ObtenerProductoPorId(string id)
        {
            var filter = Builders<Producto>.Filter.Eq(p => p.Id, id);

            var producto = await _collection.Find(filter).FirstOrDefaultAsync();

            return producto;
        }
    }
}
