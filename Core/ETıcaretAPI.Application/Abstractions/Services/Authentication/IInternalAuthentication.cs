using ETıcaretAPI.Application.Dtos;

namespace ETıcaretAPI.Application.Abstractions.Services.Authentication
{
    public interface IInternalAuthentication
    {
        Task< ETıcaretAPI.Application.Dtos.Token> LoginAsync(string userNameOrEmail, string password,int accessTokenLifeTime);
        Task< ETıcaretAPI.Application.Dtos.Token> RefreshTokenLoginAsync(string refreshToken);
    }
}
