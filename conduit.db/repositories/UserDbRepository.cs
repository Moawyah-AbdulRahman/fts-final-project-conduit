using AutoMapper;
using conduit.db.models;
using conduit.domain.models;
using conduit.domain.repositories;
using Microsoft.EntityFrameworkCore;

namespace conduit.db.repositories;

public class UserDbRepository : IUserRepository
{
    private readonly ConduitDbContext dbContext;
    private readonly IMapper mapper;

    public UserDbRepository(ConduitDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public bool ContainsId(long id)
    {
        return dbContext.Users.Any(u => u.Id == id);
    }

    public bool ContainsUsername(string username)
    {
        return dbContext.Users.Any(u => u.Username == username);
    }

    public void Create(User user)
    {
        var userEntity = mapper.Map<UserEntity>(user);
        dbContext.Users.Add(userEntity);
        dbContext.SaveChanges();
        user.Id = userEntity.Id;
    }

    public User? ReadById(long id)
    {
        return mapper.Map<User?>(
                dbContext.Users.Include(u => u.PostedArticles)
                .FirstOrDefault(u=>u.Id == id)
            );
    }

    public User? ReadBasicInfoByUsername(string username)
    {
        return mapper.Map<User?>(
            dbContext.Users
                .FirstOrDefault(u => u.Username == username)
            );
    }

    public IEnumerable<User> ReadMultiple(int page, int size)
    {
        return mapper.Map<IEnumerable<User>>(
                dbContext.Users.Include(u=>u.PostedArticles)
                .OrderBy(u => u.Id)
                .Skip((page - 1) * size)
                .Take(size)
                .ToList()
            );
    }

    public void Update(User user)
    {
        dbContext.Users.Update(mapper.Map<UserEntity>(user));
        dbContext.SaveChanges();
    }
}