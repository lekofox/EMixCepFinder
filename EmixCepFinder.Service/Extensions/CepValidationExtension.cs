using System.Text.RegularExpressions;
/// <summary>
/// Provides extension methods for validating and normalizing Brazilian postal codes (CEP).
/// </summary>
public static class CepValidationExtension
{
    private const string Pattern = "^\\d{5}-\\d{3}$";
    static readonly Regex regex = new(Pattern);

    /// <summary>
    /// Determines whether the specified string is a valid Brazilian postal code (CEP).
    /// </summary>
    /// <param name="cep">The string to validate.</param>
    /// <returns><see langword="true"/> if the specified string is a valid Brazilian postal code (CEP); otherwise, <see langword="false"/>.</returns>
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

    /// <summary>
    /// Normalizes a Brazilian postal code (CEP) by adding the hyphen if it is missing.
    /// </summary>
    /// <param name="cep">The string to normalize.</param>
    /// <returns>The normalized Brazilian postal code (CEP).</returns>
    public static string NormalizeCep(this string cep) =>
        cep.Contains('-') ? cep : cep.ConvertCep();

    private static string ConvertCep(this string cep) =>
        cep.Substring(0, 5) + "-" + cep.Substring(5, 3);
}