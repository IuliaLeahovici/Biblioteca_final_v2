using System.Web;
using Biblioteca.BusinessLogic.Core;
using Biblioteca.BusinessLogic.Interfaces;
using Biblioteca.Domain.Entities.User;

namespace Biblioteca.BusinessLogic
{
    public class SessionBL : UserApi, ISession
    {
      /*  public ULoginResp UserLogin(ULoginData data)
        {
            return UserLoginAction(data);
        }

        public HttpCookie GenCookie(string loginCredential)
        {
            return Cookie(loginCredential);
        }

        public UserMinimal GetUserByCookie(string apiCookieValue)
        {
            return UserCookie(apiCookieValue);
        }*/
    }
}
