using conduit.db.models;

namespace conduit.db.repositories;

public interface IUserRepository
{
    IEnumerable<User> ReadUsersWithArticles(int page, int size);
    User? ReadUserWithPostedArticles(long id);
}
