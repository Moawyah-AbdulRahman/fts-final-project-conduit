using conduit.domain.models;

namespace conduit.domain.repositories;

public interface IUserRepository
{
    bool ContainsId(long id);
    bool ContainsUsername(string username);
    void Create(User user);
    User? ReadById(long id);
    User? ReadBasicInfoByUsername(string username);
    IEnumerable<User> ReadMultiple(int page, int size);
    void Update(User user);
}
