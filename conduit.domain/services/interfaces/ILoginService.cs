using conduit.domain.models;

namespace conduit.domain.services.interfaces;

public interface ILoginService
{
    Token Login(string username, string password);

    void Logout(Token token);

    bool IsValidToken(Token token);
}