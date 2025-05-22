using eUseControl.Data.Entities.Product;
using eUseControl.Data.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace eUseControl.BussinesLogic.Interfaces
{
    public interface ISession
    {
        Response UserLogin(ULoginData data);
        Response UserRegister(UregisterData data);
        List<UserTable> GetUsersList();
        List<BookTable> GetBooksList();
    }
}
