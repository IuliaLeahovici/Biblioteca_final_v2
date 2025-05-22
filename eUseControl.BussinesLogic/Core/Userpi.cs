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
    }
}
