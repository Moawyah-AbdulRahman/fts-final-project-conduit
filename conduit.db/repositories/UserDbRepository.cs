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

    public bool ContainsId(long id)
    {
        return dbContext.Users.Any(u => u.Id == id);
    }

    public bool ContainsUsername(string username)
    {
        return dbContext.Users.Any(u => u.Username == username);
    }

    public void CreateUser(User user)
    {
        dbContext.Users.Add(user);
        dbContext.SaveChanges();
    }

    public User? GetUser(long id)
    {
        return dbContext.Users.FirstOrDefault(u => u.Id == id);
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

    public void UpdateUser(User user)
    {
        dbContext.Users.Update(user);
        dbContext.SaveChanges();
    }
}
