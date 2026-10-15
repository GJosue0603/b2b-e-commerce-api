using b2b_e_commerce_bll.JWT;
using b2b_e_commerce_models;
using b2b_e_commerce_repository;
using b2b_e_commerce_utilities;
using Microsoft.OpenApi;

namespace b2b_e_commerce_bll
{
    public interface IServicioDeUsuarios
    {
        Task<Usuario> ObtenerUsuarioPorId(string id);
    }

    public class ServicioDeUsuarios : IServicioDeUsuarios
    {
        private readonly IRepositorioDeUsuarios _repositorioDeUsuarios;

        public ServicioDeUsuarios(IRepositorioDeUsuarios repositorioDeUsuarios, IProveedorDeTokens proveedorDeTokens)
        {
            _repositorioDeUsuarios = repositorioDeUsuarios ?? throw new ArgumentNullException(nameof(repositorioDeUsuarios));
        }

        public async Task<Usuario> ObtenerUsuarioPorId(string id)
        {
            return await _repositorioDeUsuarios.ObtenerUsuarioPorId(id);
        }
    }
}