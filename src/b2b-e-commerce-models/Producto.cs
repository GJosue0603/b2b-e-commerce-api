
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace b2b_e_commerce_models
{
    [BsonIgnoreExtraElements]
    public class Producto
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfNull]
        public string Id { get; set; }

        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfNull]
        public string CategoriaId { get; set; }

        [BsonRepresentation(BsonType.Decimal128)]
        public decimal PrecioRegular { get; set; }

        [BsonRepresentation(BsonType.Decimal128)]
        public decimal PrecioPyme { get; set; }

        public int Existencias { get; set; }
        public string ImagenUrl { get; set; }
        public bool Activo { get; set; }

        public colaborador CreadoPor { get; set; }
        public DateTime FechaCreacion { get; set; }

        public colaborador ModificadoPor { get; set; }
        public DateTime? FechaModificacion { get; set; }
    }
}
