using conduit.db.models;

namespace conduit.db.repositories;

public interface IUserRepository
{
    bool ContainsUsername(string username);
    void CreateUser(User user);
    User? GetUser(long id);
    IEnumerable<User> ReadUsersWithArticles(int page, int size);
    User? ReadUserWithPostedArticles(long id);
    void UpdateUser(User user);
}
