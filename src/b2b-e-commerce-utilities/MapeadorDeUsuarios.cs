using b2b_e_commerce_models;

namespace b2b_e_commerce_utilities
{
    public static class MapeadorDeUsuarios
    {
        public static RespuestaDeUsuario ARespuestaDeUsuario(Usuario usuario)
        {
            return new RespuestaDeUsuario
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                Apellido1 = usuario.Apellido1,
                Apellido2 = usuario.Apellido2,
                Correo = usuario.Correo,
                Telefono = usuario.Telefono,
                Rol = usuario.Rol,
                Activo = usuario.Activo,
                FechaCreacion = usuario.FechaCreacion,
                FechaActualizacion = usuario.FechaActualizacion
            };
        }

                public static RespuestaDeUsuario ARespuestaDeUsuario(RegistroPendiente registroPendiente)
        {
            return new RespuestaDeUsuario
            {
                Id = registroPendiente.Usuario.Id,
                Nombre = registroPendiente.Usuario.Nombre,
                Apellido1 = registroPendiente.Usuario.Apellido1,
                Apellido2 = registroPendiente.Usuario.Apellido2,
                Correo = registroPendiente.Usuario.Correo,
                Telefono = registroPendiente.Usuario.Telefono,
                Rol = registroPendiente.Usuario.Rol,
                Activo = registroPendiente.Usuario.Activo,
                FechaCreacion = registroPendiente.Usuario.FechaCreacion,
                FechaActualizacion = registroPendiente.Usuario.FechaActualizacion
            };
        }
    }
}