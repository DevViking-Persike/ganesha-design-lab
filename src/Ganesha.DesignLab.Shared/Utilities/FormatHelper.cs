using System.Globalization;

namespace Ganesha.DesignLab.Shared.Utilities;

public static class FormatHelper
{
    public static string FormatNumber(decimal value, int decimals = 2)
        => value.ToString($"N{decimals}", CultureInfo.CurrentCulture);

    public static string FormatPercentage(decimal value, int decimals = 1)
        => (value / 100m).ToString($"P{decimals}", CultureInfo.CurrentCulture);

    public static string FormatCurrency(decimal value, string currencyCode = "USD", int decimals = 2)
    {
        var culture = CultureInfo.GetCultureInfoByIetfLanguageTag(
            currencyCode.Equals("BRL", StringComparison.OrdinalIgnoreCase) ? "pt-BR" :
            currencyCode.Equals("EUR", StringComparison.OrdinalIgnoreCase) ? "fr-FR" :
            "en-US");

        return value.ToString($"C{decimals}", culture);
    }

    public static string FormatDate(DateTime date, string format = "dd/MM/yyyy")
        => date.ToString(format, CultureInfo.CurrentCulture);

    public static string FormatDate(DateTimeOffset date, string format = "dd/MM/yyyy")
        => date.ToString(format, CultureInfo.CurrentCulture);

    public static string TruncateText(string text, int maxLength, string ellipsis = "...")
    {
        if (string.IsNullOrEmpty(text) || text.Length <= maxLength)
            return text;

        var truncated = maxLength - ellipsis.Length;
        return truncated <= 0
            ? ellipsis[..Math.Min(maxLength, ellipsis.Length)]
            : string.Concat(text.AsSpan(0, truncated), ellipsis);
    }
}
