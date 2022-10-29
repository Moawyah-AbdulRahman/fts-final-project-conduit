using conduit.domain.models;

namespace conduit.domain.services.interfaces;

public interface IUserService
{
    void CreateUser(User user);
    User GetUser(long id);
    IEnumerable<User> GetUsers(int page, int size);
    void UpdateUser(User user);
}
