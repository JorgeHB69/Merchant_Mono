namespace InventoryService.Application.Dtos;

public class PaginatedResponseDto<T>
{
    public List<T>? Items { get; set; }
    public int TotalCount { get; set; }
    public int ExistingElements { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
}