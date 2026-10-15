namespace b2b_e_commerce_utilities;

public class CorreoDuplicadoException : Exception
{
    public CorreoDuplicadoException() : base("El correo electrónico ingresado ya está registrado.")
    {

    }
}