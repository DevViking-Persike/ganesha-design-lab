using System.Text;

namespace Ganesha.DesignLab.Shared.Extensions;

public sealed class CssClassBuilder
{
    private readonly List<string> _classes = [];

    private CssClassBuilder(string? baseClass)
    {
        if (!string.IsNullOrWhiteSpace(baseClass))
            _classes.Add(baseClass.Trim());
    }

    public static CssClassBuilder Create(string? baseClass = null) => new(baseClass);

    public CssClassBuilder AddClass(string? cssClass, bool when = true)
    {
        if (when && !string.IsNullOrWhiteSpace(cssClass))
            _classes.Add(cssClass.Trim());

        return this;
    }

    public CssClassBuilder AddClass(string? cssClass, Func<bool> when)
    {
        if (!string.IsNullOrWhiteSpace(cssClass) && when())
            _classes.Add(cssClass.Trim());

        return this;
    }

    public string Build()
    {
        var sb = new StringBuilder();

        foreach (var cls in _classes)
        {
            if (sb.Length > 0) sb.Append(' ');
            sb.Append(cls);
        }

        return sb.ToString();
    }

    public override string ToString() => Build();
}
