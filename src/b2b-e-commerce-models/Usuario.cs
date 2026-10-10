using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace b2b_e_commerce_models;

[BsonIgnoreExtraElements]
public class Usuario
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonIgnoreIfNull]
    public string Id { get; set; }
    public string Nombre { get; set; }
    public string Apellido1 { get; set; }
    public string Apellido2 { get; set; }
    public string Correo { get; set; }
    public string ContrasenaHash { get; set; }
    public string Telefono { get; set; }
    public RolUsuario Rol { get; set; }
    public bool Activo { get; set; }
    public bool FechaCreacion { get; set; }
    public bool FechaActualizacion { get; set; }
}