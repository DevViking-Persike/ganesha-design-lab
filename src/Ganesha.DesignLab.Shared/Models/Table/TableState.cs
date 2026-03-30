namespace Ganesha.DesignLab.Shared.Models.Table;

public record TableState(
    int CurrentPage,
    int PageSize,
    int TotalItems,
    string? SortColumn,
    bool SortDescending)
{
    public int TotalPages => PageSize > 0 ? (int)Math.Ceiling((double)TotalItems / PageSize) : 0;
    public bool HasPreviousPage => CurrentPage > 1;
    public bool HasNextPage => CurrentPage < TotalPages;
}
