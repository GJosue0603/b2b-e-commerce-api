using b2b_e_commerce_models;
using b2b_e_commerce_repository;
using MongoDB.Bson;

namespace b2b_e_commerce_bll;

public interface IServicioDeUsuarios
{
    Task<Usuario> ObtenerUsuarioPorId(string id);
    Task<string> Login(LoginDeUsuario loginDeUsuario);
}

public class ServicioDeUsuarios : IServicioDeUsuarios
{
    private readonly IRepositorioDeUsuarios _repositorioDeUsuarios;
    private readonly IProveedorDeTokens _proveedorDeTokens;

    public ServicioDeUsuarios(IRepositorioDeUsuarios repositorioDeUsuarios, IProveedorDeTokens proveedorDeTokens)
    {
        _repositorioDeUsuarios = repositorioDeUsuarios ?? throw new ArgumentNullException(nameof(repositorioDeUsuarios));
        _proveedorDeTokens = proveedorDeTokens ?? throw new ArgumentNullException(nameof(proveedorDeTokens));
    }

    public async Task<Usuario> ObtenerUsuarioPorId(string id)
    {
        return await _repositorioDeUsuarios.ObtenerUsuarioPorId(id);
    }

    public async Task<string> Login(LoginDeUsuario loginDeUsuario)
    {
        // TODO: Validar que los datos no estén vacíos o nulos

        // TODO: Validar que el usuario exista en la base de datos y que la contraseña sea correcta

        // TODO: Obtener el usuario de la base de datos
        var usuario = new Usuario
        {
            Id = ObjectId.GenerateNewId().ToString(),
            Nombre = "Sebastián",
            Apellido1 = "Sánchez",
            Apellido2 = null,
            Correo = "sshernandez027@gmail.com",
            ContrasenaHash = "testing123",
            Telefono = "+506 8314-6362",
            Rol = RolUsuario.Comprador,
            Activo = true,
            FechaCreacion = DateTime.UtcNow,
            FechaActualizacion = DateTime.UtcNow
        };

        // TODO: Generar un token JWT y devolverlo al usuario
        var token = _proveedorDeTokens.CrearToken(usuario);

        return token;
    }
}