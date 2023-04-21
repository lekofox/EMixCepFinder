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
    /// <param name="postalCode">The string to validate.</param>
    /// <returns><see langword="true"/> if the specified string is a valid Brazilian postal code (CEP); otherwise, <see langword="false"/>.</returns>
    public static bool IsValid(this string postalCode) =>
        ValidateCep(postalCode);

    private static bool ValidateCep(string postalCode)
    {
        if (postalCode.IsValidLength())
        {
            return regex.IsMatch(postalCode);
        }

        return false;
    }

    private static bool IsValidLength(this string postalCode) =>
        postalCode.Length == 9;

    /// <summary>
    /// Normalizes a Brazilian postal code (CEP) by adding the hyphen if it is missing.
    /// </summary>
    /// <param name="postalCode">The string to normalize.</param>
    /// <returns>The normalized Brazilian postal code (CEP).</returns>
    public static string NormalizeCep(this string postalCode) =>
        postalCode.Contains('-') ? postalCode : postalCode.ConvertCep();

    private static string ConvertCep(this string postalCode) =>
        postalCode.Substring(0, 5) + "-" + postalCode.Substring(5, 3);
}