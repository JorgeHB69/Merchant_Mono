using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;
using merchant_api.Business.Dtos.Auth;
using merchant_api.Business.Services.Auth.Interfaces;

namespace merchant_api.Business.Services.Auth.Concretes;

public class JwtDecoder : IJwtDecoder
{
    public Task<AuthToken?> DecodeJwtToken(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);

        var jsonPayload = jwtToken.Payload.SerializeToJson();

        var authToken = JsonSerializer.Deserialize<AuthToken>(jsonPayload);

        return Task.FromResult(authToken);
    }
}
