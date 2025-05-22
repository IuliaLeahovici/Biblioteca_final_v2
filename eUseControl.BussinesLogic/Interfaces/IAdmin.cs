using eUseControl.Data.Entities.Admin;
using eUseControl.Data.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUseControl.BussinesLogic.Interfaces
{
    public interface IAdmin
    {
        Response AddUser(AddUserData user);
        void DeleteUser(int id);
        Response EditUser(EditUserData data);

        Response AddBook(AddBookData book);
        void DeleteBook(int id);
        Response EditBook(EditBookData data);
    }
}
