using eUseControl.BussinesLogic.Core;
using eUseControl.BussinesLogic.Interfaces;
using eUseControl.Data.Entities.Admin;
using eUseControl.Data.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUseControl.BussinesLogic.AppBL
{
    public class AdminBL : AdminApi, IAdmin
    {
        public Response AddBook(AddBookData book)
        {
            return AddBookAction(book);
        }
        public void DeleteBook(int id)
        {
            DeleteBookAction(id);
        }
        public Response EditBook(EditBookData data)
        {
            return EditBookAction(data);
        }
    }
}
