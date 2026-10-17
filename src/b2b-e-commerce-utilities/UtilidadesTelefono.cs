using PhoneNumbers;

namespace b2b_e_commerce_utilities
{
    public static class UtilidadesTelefono
    {
        private static readonly PhoneNumberUtil _administradorTelefonos = PhoneNumberUtil.GetInstance();

        public static bool EsNumeroValido(string numeroBruto, string regionPorDefecto = "CR")
        {
            if (string.IsNullOrWhiteSpace(numeroBruto)) return false;

            try
            {
                // 1. Limpieza inicial: Si el usuario escribe "00" en lugar de "+", lo convertimos a "+"
                string entradaSanitizada = NormalizarNumero(numeroBruto);

                // 2. Procesar el texto. La librería ignora automáticamente los paréntesis, espacios y guiones.
                // Si detecta un '+', identifica el país de forma automática e ignora el parámetro 'regionPorDefecto'.
                PhoneNumber numeroProcesado = _administradorTelefonos.Parse(entradaSanitizada, regionPorDefecto);

                // 3. Realiza la validación profunda de la estructura, longitud mínima y patrones lógicos del país
                return _administradorTelefonos.IsValidNumber(numeroProcesado);
            }
            catch (NumberParseException)
            {
                // Se ejecuta si el texto contiene caracteres inválidos, longitud absurda o un código de país inexistente
                return false;
            }
        }

        /// <summary>
        /// Convierte un número ya validado a formato estándar E.164 (ej: +525512345678), ideal para guardar en bases de datos.
        /// </summary>
        public static string FormatearParaBaseDeDatos(string numeroBruto, string regionPorDefecto = "CR")
        {
            if (string.IsNullOrWhiteSpace(numeroBruto))
            {
                return string.Empty; // Retorna vacío si no se pudo procesar
            }

            try
            {
                var numeroNormalizado = NormalizarNumero(numeroBruto);

                PhoneNumber numeroProcesado = _administradorTelefonos.Parse(numeroBruto, regionPorDefecto);
                if (_administradorTelefonos.IsValidNumber(numeroProcesado))
                {
                    return _administradorTelefonos.Format(numeroProcesado, PhoneNumberFormat.E164);
                }
            }
            catch (NumberParseException) { }

            return string.Empty; // Retorna vacío si no se pudo procesar
        }

        private static string NormalizarNumero(string numeroBruto)
        {
            var numeroNormalizado = numeroBruto.Trim();

            if (numeroNormalizado.StartsWith("00"))
            {
                numeroNormalizado = "+" + numeroNormalizado[2..];
            }

            return numeroNormalizado;
        }
    }
}