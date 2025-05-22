using eUseControl.BussinesLogic.AppBL;
using eUseControl.Data.Entities.Admin;
using eUseControl.Data.Entities.Product;
using eUseControl.Data.Entities.User;
using eUseControl.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUseControl.BussinesLogic.Core
{
    public class AdminApi
    {
        //adăugare carte 
        internal Response AddBookAction(AddBookData book)
        {
            using (var db = new TableContext())
            {
                BookTable existingBook = db.Books.FirstOrDefault(u => u.Name == book.Name);
                if (existingBook != null)
                {
                    return new Response { Status = false, StatusMsg = "Cartea deja există." };
                }

                var newBook = new BookTable
                {
                    Name = book.Name,
                    Price = book.Price,
                    Description = book.Description,
                    Author = book.Description,
                    Edit = book.Edit,
                    Type = book.Type,
                    Bought = 0,
                    Image = book.Name + ".png"
                };
                db.Books.Add(newBook);
                db.SaveChanges();
            }
            return new Response { Status = true };
        }
        //ștergere carte 
        internal void DeleteBookAction(int id)
        {
            using (var db = new TableContext())
            {
                BookTable book = db.Books.FirstOrDefault(u => u.Id == id);
                db.Books.Remove(book);
                db.SaveChanges();
            }
        }
        //editare carte 
    }
}
