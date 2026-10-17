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
        public string Nombre { get; set; } = string.Empty;
        public string Apellido1 { get; set; } = string.Empty;
        public string Apellido2 { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string ContrasenaHash { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string CodigoHash { get; set; }
        public DateTime CodigoExpiracion { get; set; }
        public DateTime FechaExpiracion { get; set; }
        public int IntentosFallidos { get; set; }
    }
}