namespace Application.Pagination;
public class PaginationInfo
{
    public int PageNumber { get; set; }

    public int TotalPageCount { get; set; }

    public int TotalRecordCount { get; set; }

    public bool HasPreviousPage => PageNumber > 1;

    public bool HasNextPage => PageNumber < TotalPageCount;
}