using System;
using b2b_e_commerce_models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace b2b_e_commerce_repository
{
    public interface IRepositorioDeRegistrosPendientes
    {
        Task<RegistroPendiente> RegistrarUsuario(RegistroPendiente registroPendiente);
    }

    public class RepositorioDeRegistrosPendientes : IRepositorioDeRegistrosPendientes
    {
        private readonly string COLLECTION_NAME = "registros_pendientes";
        private readonly IMongoCollection<RegistroPendiente> _collection;
        public RepositorioDeRegistrosPendientes(IMongoClient client, IConfiguration configuracion)
        {
            ArgumentNullException.ThrowIfNull(client);

            var databaseName = configuracion["Mongo:Database"];

            if (string.IsNullOrWhiteSpace(databaseName))
            {
                throw new InvalidOperationException("MongoDB database name is not configured.");
            }

            _collection = client.GetDatabase(databaseName).GetCollection<RegistroPendiente>(COLLECTION_NAME);
        }

        public async Task<RegistroPendiente> RegistrarUsuario(RegistroPendiente registroPendiente)
        {
            await _collection.InsertOneAsync(registroPendiente);

            return registroPendiente;
        }

    }
}