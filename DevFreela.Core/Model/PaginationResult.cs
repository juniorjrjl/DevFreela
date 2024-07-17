namespace DevFreela.Core.Persistence.model;
public class PaginationResult<T>
{

    public PaginationResult() { }

    public PaginationResult(int page, int totalPages, int pageSize, int itemCount, ICollection<T> data)
    {
        Page = page;
        TotalPages = totalPages;
        PageSize = pageSize;
        ItemCount = itemCount;
        Data = data;
    }

    public int Page { get; private set; }
    
    public int TotalPages { get; private set; }
    
    public int PageSize { get; private set; }

    public int ItemCount { get; private set; }

    public ICollection<T> Data { get; private set; } = [];
    
}
