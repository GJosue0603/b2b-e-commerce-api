namespace b2b_e_commerce_api;

public static class ConfiguracionDeServicios
{
    public static IServiceCollection ConfigurarServicios(this IServiceCollection servicios, IConfiguration configuracion)
    {
        // Servicios de JWT
        servicios.AddSingleton<ProveedorDeTokens>(new ProveedorDeTokens(configuracion));
        
        return servicios;
    }
}