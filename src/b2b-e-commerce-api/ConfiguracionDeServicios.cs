using System.Text;
using b2b_e_commerce_bll;
using b2b_e_commerce_repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace b2b_e_commerce_api;

public static class ConfiguracionDeServicios
{
    public static IServiceCollection ConfigurarServicios(this IServiceCollection servicios, IConfiguration configuracion)
    {
        // Servicios de JWT
        servicios.AddAuthorization();
        servicios.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.TokenValidationParameters =
                new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuracion["Jwt:Emisor"],
                    ValidAudience = configuracion["Jwt:Audiencia"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuracion["Jwt:Secreto"])),
                    ClockSkew = TimeSpan.Zero
                };
        });
        servicios.AddSingleton<IProveedorDeTokens, ProveedorDeTokens>();

        // Servicios
        servicios.AddSingleton<IServicioDeUsuarios, ServicioDeUsuarios>();
        
        // Repositorios
        servicios.AddSingleton<IRepositorioDeUsuarios, RepositorioDeUsuarios>();
        
        return servicios;
    }
}