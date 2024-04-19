using ETıcaretAPI.Application.Abstractions.Services.Authentication;

namespace ETıcaretAPI.Application.Abstractions.Services
{
    public interface IAuthService:IExternalAuthentication,IInternalAuthentication
    {
      
        
    }
}
