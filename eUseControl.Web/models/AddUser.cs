using eUseControl.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eUseControl.Web.Models
{
    public class AddUser
    {
        //nume,parola,email,rol, imagine
        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public URole Level { get; set; }

        public string Image { get; set; }
    }
}