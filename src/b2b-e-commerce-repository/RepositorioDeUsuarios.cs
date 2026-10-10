using System.Diagnostics;
using b2b_e_commerce_models;
using MongoDB.Driver;

namespace b2b_e_commerce_repository;

public interface IRepositorioDeUsuarios
{
    Task<Usuario> ObtenerUsuarioPorId(string id);
}

public class RepositorioDeUsuarios : IRepositorioDeUsuarios
{
    private readonly string DATABASE_NAME = "b2b-e-commerce";
    private readonly string COLLECTION_NAME = "usuarios_dev";
    private readonly IMongoClient _client;
    private readonly IMongoCollection<Usuario> _collection;
    public RepositorioDeUsuarios(IMongoClient client)
    {
        _client = client ?? throw new ArgumentNullException(nameof(client));

        try
        {
            _collection = _client.GetDatabase(DATABASE_NAME).GetCollection<Usuario>(COLLECTION_NAME);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex, ex.Message);
        }
    }

    public async Task<Usuario> ObtenerUsuarioPorId(string id)
    {
        var filter = Builders<Usuario>.Filter.Eq(u => u.Id, id);
        
        var usuario = await _collection.Find(filter).FirstOrDefaultAsync();

        return usuario;
    }
}