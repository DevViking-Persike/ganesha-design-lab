namespace Ganesha.DesignLab.Shared.Models.Table;

public class TableColumn<TItem>
{
    public string Header { get; set; } = string.Empty;
    public Func<TItem, object?> ValueSelector { get; set; } = _ => null;
    public bool Sortable { get; set; }
    public string? Width { get; set; }
    public string? CssClass { get; set; }
}
