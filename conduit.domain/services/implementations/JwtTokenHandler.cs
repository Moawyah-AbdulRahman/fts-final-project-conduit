using conduit.domain.exceptions;
using conduit.domain.models;
using conduit.domain.services.interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace conduit.domain.services.implementations;

public class JwtTokenHandler : ITokenHandler
{
    private readonly string signingKey;

    private readonly int minutesToLive;

    private readonly JwtSecurityTokenHandler jwtSecurityHandler;

    public JwtTokenHandler(string signingKey, int minutesToLive)
    {
        this.minutesToLive = minutesToLive;
        this.signingKey = signingKey;
        this.jwtSecurityHandler = new JwtSecurityTokenHandler();
    }

    private IEnumerable<Claim> AllClaims(Token token)
    {
        if (!jwtSecurityHandler.CanReadToken(token.Value))
        {
            throw new ConduitException("Token is in wrong format", HttpResponseCode.BadRequest);
        }
        return jwtSecurityHandler.ReadJwtToken(token.Value).Claims;
    }

    public long ExtractTokenId(Token token)
    {
        return Convert.ToInt64(AllClaims(token).First(claim => claim.Type == "tokenId").Value);
    }

    public long ExtractUserId(Token token)
    {
        return Convert.ToInt64(AllClaims(token).First(claim => claim.Type == "userId").Value);
    }

    public Token Generate(long userId, long tokenId, string username)
    {
        var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, username),
        new Claim("userId", userId.ToString()),
        new Claim("tokenId", tokenId.ToString()),
    };

        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(signingKey));

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddMinutes(minutesToLive),
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha512)
            );

        return new Token(jwtSecurityHandler.WriteToken(token));
    }
}