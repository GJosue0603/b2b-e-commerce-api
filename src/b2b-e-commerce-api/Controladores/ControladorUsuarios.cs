using System.Net;
using b2b_e_commerce_models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace b2b_e_commerce_api;

[ApiController]
[Route("v1/usuarios")]
// [Authorize(Policy = "Usuarios")]
[Produces("application/json")]
public class ControladorUsuarios : Controller
{
    private readonly IConfiguration _configuracion;

    public ControladorUsuarios(IConfiguration configuracion)
    {
        _configuracion = configuracion ?? throw new ArgumentNullException(nameof(configuracion));
    }

    [HttpGet]
    [Route("")]
    [ProducesResponseType(typeof(Usuario), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> TestEndpoint()
    {
        return Ok(_configuracion["App:Nombre"] + " - " + _configuracion["App:Environment"] + " - " + _configuracion["Jwt:Secreto"]);
    }
}