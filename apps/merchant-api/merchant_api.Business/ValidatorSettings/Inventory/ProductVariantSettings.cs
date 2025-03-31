namespace merchant_api.Business.ValidatorSettings.Inventory;

public class ProductVariantSettings
{
    public int StockQuantityMin { get; set; }
    public int AttributeNameMinLenght { get; set; }
    public int AttributeNameMaxLenght { get; set; }
    public int AttributeValueMinLenght { get; set; }
    public int AttributeValueMaxLenght { get; set; }

}