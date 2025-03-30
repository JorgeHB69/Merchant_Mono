namespace merchant_api.Business.Dtos.Images;

public class CreateImageDto
{
    public Guid? ProductId { get; set; }
    public string AltText { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
}