using System.ComponentModel;

namespace b2b_e_commerce_models;

public enum RolUsuario
{
    [Description("COMPRADOR")]
    Comprador = 1,

    [Description("VENDEDOR")]
    Vendedor = 2,
    
    [Description("ADMINISTRADOR")]
    Administrador = 3
}