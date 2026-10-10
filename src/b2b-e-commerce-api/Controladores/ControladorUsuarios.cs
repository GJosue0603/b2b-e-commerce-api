using System.Net;
using b2b_e_commerce_bll;
using b2b_e_commerce_models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
    [ProducesResponseType(typeof(Usuario), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Registrar([FromBody] Usuario usuario)
    {
        // TODO: Validar que los datos no estén vacíos o nulos

        // TODO: Validar que los datos tengan un formato correcto

        // TODO: Guardar el usuario

        return Ok("Test Registrar");
    }

    [HttpPost]
    [Route("login")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(Usuario), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Login([FromBody] LoginDeUsuario loginDeUsuario)
    {
        // TODO: Validar que los datos no estén vacíos o nulos

        // TODO: Validar que el correo tenga un formato correcto

        var token = await _servicioDeUsuarios.Login(loginDeUsuario);

        return Ok(token);
    }

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(typeof(Usuario), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> ObtenerUsuario(string id)
    {
        // TODO: Validar que el ID no esté nulo

        // TODO: Validar que el ID sea un MongoDB Object Id

        // var token = await _servicioDeUsuarios.Login(loginDeUsuario);

        return Ok("Test Obtener Usuario");
    }
}