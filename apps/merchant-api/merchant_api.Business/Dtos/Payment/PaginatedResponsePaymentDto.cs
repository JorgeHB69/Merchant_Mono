namespace merchant_api.Business.Dtos.Payment;

public class PaginatedResponsePaymentDto<T>
{
    public List<T>? Items { get; set; }
    public int TotalCount { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
}