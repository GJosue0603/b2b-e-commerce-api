using b2b_e_commerce_models;
using b2b_e_commerce_repository;
using b2b_e_commerce_utilities;

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
        var usuario = await _repositorioDeUsuarios.ObtenerUsuarioPorCorreo(loginDeUsuario.Correo);

        if (usuario == null || !usuario.Activo || !usuario.CorreoVerificado || !UtilidadesContrasena.Verificar(loginDeUsuario.Contrasena, usuario.ContrasenaHash))
        {
            throw new UnauthorizedAccessException("Correo o contraseña incorrectos");
        }

        var token = _proveedorDeTokens.CrearToken(usuario);

        return token;
    }
}