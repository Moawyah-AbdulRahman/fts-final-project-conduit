using conduit.domain.models;

namespace conduit.domain.services.interfaces;

public interface ITokenHandler
{
    long ExtractTokenId(Token token);
    long ExtractUserId(Token token);
    Token Generate(long userId, long tokenId, string username);
}