using System.Text.RegularExpressions;

namespace EmixCepFinder.Service.Extensions
{
    public static class CepValidationExtension
    {
        private const string Pattern = "^\\d{5}-\\d{3}$";
        static readonly Regex regex = new(Pattern);

        public static bool IsValid(this string cep) =>
            ValidateCep(cep);

        private static bool ValidateCep(string cep)
        {
            if (cep.IsValidLength())
            {
                return regex.IsMatch(cep);
            }

            return false;
        }

        private static bool IsValidLength(this string cep) =>
            cep.Length == 9;

        public static string NormalizeCep(this string cep) =>
            cep.Contains('-') ? cep : cep.ConvertCep();

        private static string ConvertCep(this string cep) =>
            cep.Substring(0, 5) + "-" + cep.Substring(5, 3);

    }
}
