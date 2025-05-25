using AutoMapper;
using eUseControl.BussinesLogic.AppBL;
using eUseControl.Data.Entities.User;
using eUseControl.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using eUseControl.Model;
using eUseControl.Data.Entities.Product;

namespace eUseControl.BussinesLogic.Core
{
    public class UserApi
    {
        //userloginaction!
        internal Response UserLoginAction(ULoginData data)
        {
            UserTable result;
            var validate = new EmailAddressAttribute();
            if (validate.IsValid(data.Email))
            {
                //var pass = LoginHelper.HashGen(data.Password);
                //System.Diagnostics.Debug.WriteLine("Password hash: " + pass);
                using (var db = new TableContext())
                {
                    result = db.Users.FirstOrDefault(u => u.Email == data.Email && u.Password == data.Password);
                }

                if (result == null)
                {
                    return new Response { Status = false, StatusMsg = "eror" };
                }

                using (var todo = new TableContext())
                {
                    result.LastIp = data.LoginIp;
                    result.LastLogin = data.LoginDateTime;
                    todo.Entry(result).State = EntityState.Modified;
                    todo.SaveChanges();
                }
                return new Response { Status = true };
            }
            else
                return new Response { Status = false };
        }
        internal Response UserRegisterAction(UregisterData data)
        {
            UserTable existingEmail;
            UserTable existingUsername;
            var validate = new EmailAddressAttribute();
            if (validate.IsValid(data.Email))
            {
                using (var db = new TableContext())
                {
                    existingEmail = db.Users.FirstOrDefault(u => u.Email == data.Email);
                    existingUsername = db.Users.FirstOrDefault(u => u.Username == data.Username);
                }

                if (existingEmail != null)
                {
                    return new Response { Status = false, StatusMsg = "Eror." };
                }
                if (existingUsername != null)
                {
                    return new Response { Status = false, StatusMsg = "Eror." };
                }

                //var pass = LoginHelper.HashGen(data.Password);
                var newUser = new UserTable
                {
                    Email = data.Email,
                    Username = data.Username,
                    Password = data.Password,
                    LastIp = data.LoginIp,
                    LastLogin = data.LoginDateTime,
                    Level = (URole)0,
                };

                using (var todo = new TableContext())
                {
                    todo.Users.Add(newUser);
                    todo.SaveChanges();
                }
                return new Response { Status = true };
            }
            else
                return new Response { Status = false };
        }
        internal List<UserTable> GetUsersListAction()
        {
            List<UserTable> users;
            using (var db = new TableContext())
            {
                users = db.Users.ToList();
            }
            return users;
        }

        internal List<BookTable> GetBooksListAction()
        {
            List<BookTable> books;
            using (var db = new TableContext())
            {
                books = db.Books.ToList();
            }
            return books;
        }
        internal HttpCookie Cookie(string loginCredential)
        {
            var apiCookie = new HttpCookie("X-KEY")
            {
                Value = eUseControl.Model.CookieGenerator.Create(loginCredential)
            };

            using (var db = new TableContext())
            {
                Session curent;
                var validate = new EmailAddressAttribute();
                if (validate.IsValid(loginCredential))
                {
                    curent = (from e in db.Sessions where e.Username == loginCredential select e).FirstOrDefault();
                }
                else
                {
                    curent = (from e in db.Sessions where e.Username == loginCredential select e).FirstOrDefault();
                }

                if (curent != null)
                {
                    curent.CookieString = apiCookie.Value;
                    curent.ExpireTime = DateTime.Now.AddMinutes(60);
                    using (var todo = new TableContext())
                    {
                        todo.Entry(curent).State = EntityState.Modified;
                        todo.SaveChanges();
                    }
                }
                else
                {
                    db.Sessions.Add(new Session
                    {
                        Username = loginCredential,
                        CookieString = apiCookie.Value,
                        ExpireTime = DateTime.Now.AddMinutes(60)
                    });
                    db.SaveChanges();
                }
            }

            return apiCookie;
        }

        internal UserMinimal UserCookie(string cookie)
        {
            Session session;
            UserTable curentUser;

            using (var db = new TableContext())
            {
                session = db.Sessions.FirstOrDefault(s => s.CookieString == cookie && s.ExpireTime > DateTime.Now);
            }

            if (session == null) return null;
            using (var db = new TableContext())
            {
                var validate = new EmailAddressAttribute();
                if (validate.IsValid(session.Username))
                {
                    curentUser = db.Users.FirstOrDefault(u => u.Email == session.Username);
                }
                else
                {
                    curentUser = db.Users.FirstOrDefault(u => u.Username == session.Username);
                }
            }

            if (curentUser == null) return null;
            var userminimal = Mapper.Map<UserMinimal>(curentUser);

            return userminimal;
        }
    }
}
