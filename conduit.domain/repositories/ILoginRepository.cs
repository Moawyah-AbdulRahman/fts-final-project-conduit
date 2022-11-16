namespace conduit.domain.repositories;

public interface ILoginRepository
{
    long Login(long userId);

    void Logout(long userId);

    bool IsLoggedIn(long userId, long tokenId);
}
