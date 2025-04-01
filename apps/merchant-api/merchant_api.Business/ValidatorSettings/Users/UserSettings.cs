namespace merchant_api.Business.ValidatorSettings.Users;

public class UserSettings
{
    public int UserNameMinLength { get; set; }
    public int UserNameMaxLength { get; set; }
    public string? EmailRegex { get; set; }
}