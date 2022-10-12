using conduit.db.models;
using Microsoft.EntityFrameworkCore;

namespace conduit.db.repositories;

public class UserDbRepository : IUserRepository
{
    private readonly ConduitDbContext dbContext;

    public UserDbRepository(ConduitDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public IEnumerable<User> ReadUsersWithArticles(int page, int size)
    {
        return dbContext.Users
            .Include(u => u.PostedArticles)
            .Skip((page - 1) * size)
            .Take(size)
            .ToList();
    }

    public User? ReadUserWithPostedArticles(long id)
    {
        return dbContext.Users
            .Include(u => u.PostedArticles)
            .FirstOrDefault(u => u.Id == id);
    }
}
