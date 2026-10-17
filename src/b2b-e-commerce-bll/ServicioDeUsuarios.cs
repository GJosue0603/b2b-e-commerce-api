using b2b_e_commerce_models;
using b2b_e_commerce_repository;

namespace b2b_e_commerce_bll
{
    public interface IServicioDeUsuarios
    {
        Task<Usuario> ObtenerUsuarioPorId(string id);
        Task<ResultadoPaginado<RespuestaDeUsuario>> ObtenerListaDeUsuarios(
            string? nombre = null,
            string? correo = null,
            bool? estadoDeUsuario = null,
            int numeroDePagina = 1,
            int cantidadPorPagina = 25
        );
    }

    public class ServicioDeUsuarios : IServicioDeUsuarios
    {
        private readonly IRepositorioDeUsuarios _repositorioDeUsuarios;

        public ServicioDeUsuarios(IRepositorioDeUsuarios repositorioDeUsuarios)
        {
            _repositorioDeUsuarios = repositorioDeUsuarios ?? throw new ArgumentNullException(nameof(repositorioDeUsuarios));
        }

        public async Task<Usuario> ObtenerUsuarioPorId(string id)
        {
            return await _repositorioDeUsuarios.ObtenerUsuarioPorId(id);
        }

        public async Task<ResultadoPaginado<RespuestaDeUsuario>> ObtenerListaDeUsuarios(
            string? nombre = null,
            string? correo = null,
            bool? estadoDeUsuario = null,
            int numeroDePagina = 1,
            int cantidadPorPagina = 25
        )
        {
            return await _repositorioDeUsuarios.ObtenerListaDeUsuarios(nombre, correo, estadoDeUsuario, numeroDePagina, cantidadPorPagina);
        }
    }
}