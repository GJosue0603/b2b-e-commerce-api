using System.ComponentModel;

namespace b2b_e_commerce_utilities
{
    public static class UtilidadesEnums
    {
        public static string ObtenerDescripcionEnBaseAlValor(Enum valor)
        {
            DescriptionAttribute atributo = valor.GetType()
                .GetField(valor.ToString())
                .GetCustomAttributes(typeof(DescriptionAttribute), false)
                .SingleOrDefault() as DescriptionAttribute;
            return atributo == null ? valor.ToString() : atributo.Description;
        }

        public static T ObtenerValorEnBaseALaDescription<T>(string descripcion) where T : Enum
        {
            foreach (var campo in typeof(T).GetFields())
            {
                if (Attribute.GetCustomAttribute(campo,
                typeof(DescriptionAttribute)) is DescriptionAttribute atributo)
                {
                    if (atributo.Description == descripcion)
                        return (T)campo.GetValue(null);
                }
                else
                {
                    if (campo.Name == descripcion)
                        return (T)campo.GetValue(null);
                }
            }

            throw new ArgumentException("No se encontró.", nameof(descripcion));
        }
    }
}