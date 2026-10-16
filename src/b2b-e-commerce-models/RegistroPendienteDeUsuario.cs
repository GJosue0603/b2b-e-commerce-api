using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace b2b_e_commerce_models
{
    [BsonIgnoreExtraElements]
    public class RegistroPendiente
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfNull]
        public string Id { get; set; }
        public Usuario Usuario { get; set; }
        public string CodigoHash { get; set; }
        public DateTime FechaExpiracion { get; set; }
        public int IntentosFallidos { get; set; }
    }
}