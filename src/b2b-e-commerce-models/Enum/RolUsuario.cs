using System.ComponentModel;

namespace b2b_e_commerce_models;

public enum RolUsuario
{
    [Description("Comprador")]
    Comprador = 1,

    [Description("Vendedor")]
    Vendedor = 2,
    
    [Description("Administrador")]
    Administrador = 3
}