using conduit.domain.models;
using conduit.domain.repositories;
using conduit.domain.exceptions;
using conduit.domain.services.interfaces;

namespace conduit.domain.services.implementations;

public class UserService : IUserService
{
    private readonly IUserRepository repository;

    public UserService(IUserRepository repository)
    {
        this.repository = repository;
    }

    public void CreateUser(User user)
    {
        if(user.Id is not null)
        {
            throw new ArgumentException("cannot create user that already have an id, try update");
        }

        if (repository.ContainsUsername(user.Username))
        {
            throw new ConduitException("username is taken, try out another one", 
                HttpResponseCode.BadRequest);
        }
        repository.Create(user);
    }

    public User GetUser(long id)
    {
        return repository.ReadById(id) ?? 
            throw new ConduitException("trying to get a user that does not exist", 
            HttpResponseCode.NotFound);
    }

    public IEnumerable<User> GetUsers(int page, int size)
    {
        //TODO:: make max size configurable 
        size = Math.Min(50,Math.Max(1,size));
        page = Math.Max(page, 1);

        return repository.ReadMultiple(page, size);
    }

    public void UpdateUser(User user)
    {
        if(user.Id is null || !repository.ContainsId(user.Id.Value))
        {
            throw new ConduitException("trying to update a user that does not exists", 
                HttpResponseCode.NotFound);
        }

        repository.Update(user);
    }
}