using merchant_api.Business.Dtos.Auth;

namespace merchant_api.Business.Services.Auth.Interfaces;

public interface IJwtDecoder
{
    Task<AuthToken?> DecodeJwtToken(string token);
}
