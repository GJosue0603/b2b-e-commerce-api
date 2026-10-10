using System.Diagnostics;
using b2b_e_commerce_models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace b2b_e_commerce_repository;

public interface IRepositorioDeUsuarios
{
    Task<Usuario> ObtenerUsuarioPorId(string id);

    Task<Usuario> ObtenerUsuarioPorCorreo(string correo);
}

public class RepositorioDeUsuarios : IRepositorioDeUsuarios
{
    private readonly string COLLECTION_NAME = "usuarios";
    private readonly IMongoClient _client;
    private readonly IMongoCollection<Usuario> _collection;
    public RepositorioDeUsuarios(IMongoClient client, IConfiguration configuracion)
    {
        _client = client ?? throw new ArgumentNullException(nameof(client));

        var DATABASE_NAME = configuracion["Mongo:Database"] ?? throw new ArgumentNullException(nameof(configuracion), "Database name is not configured.");

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

    public async Task<Usuario> ObtenerUsuarioPorCorreo(string correo)
    {
        var filter = Builders<Usuario>.Filter.Eq(u => u.Correo, correo);

        var usuario = await _collection.Find(filter).FirstOrDefaultAsync();

        return usuario;
    }

}