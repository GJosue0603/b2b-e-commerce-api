using b2b_e_commerce_bll;
using b2b_e_commerce_models;
using b2b_e_commerce_utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.Net;

namespace b2b_e_commerce_api;

[ApiController]
[Route("v1/usuarios")]
[Authorize]
[Produces("application/json")]
public class ControladorUsuarios : Controller
{
    private readonly IServicioDeUsuarios _servicioDeUsuarios;

    public ControladorUsuarios(IServicioDeUsuarios servicioDeUsuarios)
    {
        _servicioDeUsuarios = servicioDeUsuarios ?? throw new ArgumentNullException(nameof(servicioDeUsuarios));
    }

    [HttpPost]
    [Route("registrar")]
    [AllowAnonymous]
    // [ApiExplorerSettings(IgnoreApi = true)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Registrar([FromBody] RegistroDeUsuario registroDeUsuario)
    {
        if (registroDeUsuario == null)
        {
            return BadRequest("El usuario a registrar no puede estar nulo.");
        }

        if (string.IsNullOrWhiteSpace(registroDeUsuario.Nombre) ||
            string.IsNullOrWhiteSpace(registroDeUsuario.Apellido1) ||
            string.IsNullOrWhiteSpace(registroDeUsuario.Correo) ||
            string.IsNullOrWhiteSpace(registroDeUsuario.Contrasena) ||
            string.IsNullOrWhiteSpace(registroDeUsuario.Telefono))
        {
            return BadRequest("El Nombre, Apellido1, Correo, Contraseña y Teléfono son requeridos para el registro del usuario.");
        }

        if (!UtilidadesCorreo.EsCorreoValido(registroDeUsuario.Correo))
        {
            return BadRequest("El formato del correo electrónico no es válido.");
        }

        if (!UtilidadesTelefono.EsNumeroValido(registroDeUsuario.Telefono))
        {
            return BadRequest("El formato del número de teléfono no es válido.");
        }

        if (!UtilidadesContrasena.EsContrasenaValida(registroDeUsuario.Contrasena))
        {
            return BadRequest("La contraseña debe contener al menos 8 caractéres, una mayúscula y un número.");
        }

        try
        {
            var res = await _servicioDeUsuarios.RegistrarUsuario(registroDeUsuario);

            return Created("Usuario Creado", MapeadorDeUsuarios.ARespuestaDeUsuario(res));
        }
        catch (CorreoDuplicadoException ex)
        {
            return Conflict(ex.Message);
        }
    }

    [HttpPost]
    [Route("login")]
    [AllowAnonymous]
    // [ApiExplorerSettings(IgnoreApi = true)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Login([FromBody] LoginDeUsuario loginDeUsuario)
    {
        if (string.IsNullOrWhiteSpace(loginDeUsuario.Correo))
        {
            return BadRequest("El Correo es requerido.");
        }

        if (string.IsNullOrWhiteSpace(loginDeUsuario.Contrasena))
        {
            return BadRequest("La Contraseña es requerida.");
        }

        if (!UtilidadesCorreo.EsCorreoValido(loginDeUsuario.Correo))
        {
            return BadRequest("El formato del correo electrónico no es válido.");
        }

        try
        {
            var token = await _servicioDeUsuarios.Login(loginDeUsuario);
            return Ok(token);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(ex.Message);
        }
    }

    [HttpGet]
    [Route("{usuarioId}")]
    [Authorize(Roles = "Administrador")]
    [ProducesResponseType(typeof(RespuestaDeUsuario), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> ObtenerUsuario(string usuarioId)
    {
        if (string.IsNullOrWhiteSpace(usuarioId))
        {
            return BadRequest("El UsuarioId es requerido.");
        }

        if (!ObjectId.TryParse(usuarioId, out var id))
        {
            return BadRequest("UsuarioId debe ser un ObjectId (24 hex string) válido.");
        }

        var res = await _servicioDeUsuarios.ObtenerUsuarioPorId(usuarioId);

        if (res != null)
        {
            return Ok(MapeadorDeUsuarios.ARespuestaDeUsuario(res));
        }

        return NotFound("Usuario no encontrado.");
    }
}