using b2b_e_commerce_bll;
using b2b_e_commerce_models;
using b2b_e_commerce_utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.Net;

namespace b2b_e_commerce_api.Controladores
{
    [ApiController]
    [Route("v1/usuarios")]
    [Authorize]
    [Produces("application/json")]
    public class ControladorDeUsuarios : Controller
    {
        private readonly IServicioDeUsuarios _servicioDeUsuarios;

        public ControladorDeUsuarios(IServicioDeUsuarios servicioDeUsuarios)
        {
            _servicioDeUsuarios = servicioDeUsuarios ?? throw new ArgumentNullException(nameof(servicioDeUsuarios));
        }

        [HttpGet]
        [Route("{usuarioId}")]
        [Authorize(Roles = Constantes.ADMINISTRADOR)]
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

        [HttpGet]
        [Route("")]
        [Authorize(Roles = Constantes.ADMINISTRADOR)]
        [ProducesResponseType(typeof(ResultadoPaginado<RespuestaDeUsuario>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ObtenerListaDeUsuarios(
            string? nombre = null,
            string? correo = null,
            bool? estadoDeUsuario = null,
            int numeroDePagina = 1,
            int cantidadPorPagina = 25
        )
        {
            if (numeroDePagina < 1)
            {
                return BadRequest("El número de página tiene que ser mayor o igual a 1.");
            }

            if (cantidadPorPagina > 100)
            {
                return BadRequest("La cantidad de usuarios por página tiene que ser menor o igual a 100.");
            }

            var res = await _servicioDeUsuarios.ObtenerListaDeUsuarios(nombre, correo, estadoDeUsuario, numeroDePagina, cantidadPorPagina);

            return Ok(res);
        }
    }
}