using b2b_e_commerce_bll.JWT;
using b2b_e_commerce_models;
using b2b_e_commerce_models.Enum;
using b2b_e_commerce_repository;
using b2b_e_commerce_utilities;
using b2b_e_commerce_utilities.Excepciones;
using Microsoft.OpenApi;

namespace b2b_e_commerce_bll
{
    public interface IServicioDeAutenticacion
    {
        Task<Usuario> RegistrarUsuario(RegistroDeUsuario registroDeUsuario);
        Task<string> Login(LoginDeUsuario loginDeUsuario);
    }

    public class ServicioDeAutenticacion : IServicioDeAutenticacion
    {
        private readonly IRepositorioDeUsuarios _repositorioDeUsuarios;
        private readonly IProveedorDeTokens _proveedorDeTokens;

        public ServicioDeAutenticacion(IRepositorioDeUsuarios repositorioDeUsuarios, IProveedorDeTokens proveedorDeTokens)
        {
            _repositorioDeUsuarios = repositorioDeUsuarios ?? throw new ArgumentNullException(nameof(repositorioDeUsuarios));
            _proveedorDeTokens = proveedorDeTokens ?? throw new ArgumentNullException(nameof(proveedorDeTokens));
        }

        public async Task<Usuario> RegistrarUsuario(RegistroDeUsuario registroDeUsuario)
        {
            // Normalizar correo y hashear contraseña
            var correoNormalizado = registroDeUsuario.Correo.Normalizado();
            var contrasenaHash = UtilidadesContrasena.Hashear(registroDeUsuario.Contrasena);

            // Verificar que no exista un usuario con este correo la base de datos
            var usuarioPorCorreo = await _repositorioDeUsuarios.ObtenerUsuarioPorCorreo(correoNormalizado);
            if (usuarioPorCorreo != null)
            {
                throw new CorreoDuplicadoException();
            }

            // Se utiliza una variable para exactamente el mismo valor en ambos campos de fecha.
            // Si se utilizara DateTime.UtcNow por cada campo, podría existir una diferencia de milisegundos
            var horaYFechaActual = DateTime.UtcNow;

            // Crear el modelo de usuario
            var usuario = new Usuario
            {
                Nombre = registroDeUsuario.Nombre,
                Apellido1 = registroDeUsuario.Apellido1,
                Apellido2 = registroDeUsuario.Apellido2,
                Correo = correoNormalizado,
                ContrasenaHash = contrasenaHash,
                Telefono = UtilidadesTelefono.FormatearParaBaseDeDatos(registroDeUsuario.Telefono),
                Activo = true,
                Rol = UtilidadesEnums.ObtenerDescripcionEnBaseAlValor(RolUsuario.Comprador),
                FechaCreacion = horaYFechaActual,
                FechaActualizacion = horaYFechaActual
            };

            // Guardar el usuario en base de datos
            var res = await _repositorioDeUsuarios.RegistrarUsuario(usuario);

            return res;
        }

        public async Task<string> Login(LoginDeUsuario loginDeUsuario)
        {
            // Normalizar correo
            var correoNormalizado = loginDeUsuario.Correo.Normalizado();
            var usuario = await _repositorioDeUsuarios.ObtenerUsuarioPorCorreo(correoNormalizado);

            if (usuario == null || !usuario.Activo || !UtilidadesContrasena.Verificar(loginDeUsuario.Contrasena, usuario.ContrasenaHash))
            {
                throw new UnauthorizedAccessException("Correo o contraseña incorrectos");
            }

            var token = _proveedorDeTokens.CrearToken(usuario);

            return token;
        }
    }
}