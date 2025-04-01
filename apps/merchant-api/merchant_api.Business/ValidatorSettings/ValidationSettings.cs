using merchant_api.Business.ValidatorSettings.Users;

namespace merchant_api.Business.ValidatorSettings.Inventory;

public class ValidationSettings
{
    public CategorySettings Category { get; set; } = new();
    public ImageSettings Image { get; set; } = new();
    public VariantSettings Variant { get; set; } = new();
    public ProductVariantSettings ProductVariant { get; set; } = new();
    public ProductSettings Product { get; set; } = new();
    public UserSettings User { get; set; } = new ();
    public StoreSettings Store { get; set; } = new ();
    public ContactUsSettings ContactUs { get; set; } = new ();
}