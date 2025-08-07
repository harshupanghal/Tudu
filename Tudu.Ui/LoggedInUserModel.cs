using System.Security.Claims;

namespace BlazorAuthNoIdentity;

public record LoggedInUserModel(int Id, string UserName)
    {
    public Claim[] ToClaims() =>
        [
            new Claim(ClaimTypes.NameIdentifier, Id.ToString()),
            new Claim(ClaimTypes.Name, UserName),
        ];
    }