using conduit.domain.exceptions;
using conduit.domain.models;
using conduit.domain.repositories;
using conduit.domain.services.interfaces;

namespace conduit.domain.services.implementations;

public class JwtLoginService : ILoginService
{
    private readonly IUserRepository userRepository;

    private readonly ILoginRepository loginRepository;

    private readonly ITokenHandler tokenHandler;

    public JwtLoginService(IUserRepository userRepository, ILoginRepository loginRepository, 
                            ITokenHandler tokenHandler)
    {
        this.userRepository = userRepository;
        this.loginRepository = loginRepository;
        this.tokenHandler = tokenHandler;
    }

    public bool IsValidToken(Token token)
    {
        return loginRepository.IsLoggedIn(tokenHandler.ExtractUserId(token), tokenHandler.ExtractTokenId(token));
    }
    
    public Token Login(string username, string password)
    {
        if (!userRepository.ContainsUsername(username))
        {
            throw new ConduitException("Username not found.", HttpResponseCode.NotFound);
        }

        var userInDb = userRepository.ReadBasicInfoByUsername(username);

        if (!userInDb!.Password.Equals(password))
        {
            throw new ConduitException("Wrong password.", HttpResponseCode.Unauthorized);
        }
        var userId = userInDb.Id!.Value;

        var tokenId = loginRepository.Login(userId);

        return tokenHandler.Generate(userId, tokenId, username);
    }

    public void Logout(Token token)
    {
        loginRepository.Logout(tokenHandler.ExtractUserId(token));
    }
}
