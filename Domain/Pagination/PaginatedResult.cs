namespace Domain.Pagination;
public class PaginatedResult<T> : PaginationInfo where T : class
{
    public List<T> Datas { get; set; }

    public PaginatedResult(List<T> items, int totalCount, int pageNumber, int pageSize)
    {
        if (pageNumber != 0)
        {
            PageNumber = pageNumber;
            TotalRecordCount = totalCount;
            TotalPageCount = pageSize == 0 ? 1 : (totalCount + pageSize - 1) / pageSize;
        }
        else
        {
            PageNumber = 1;
            TotalRecordCount = items.Count;
            TotalPageCount = 1;
        }

        Datas = items;
    }
}