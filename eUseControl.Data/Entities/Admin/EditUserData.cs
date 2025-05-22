using eUseControl.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUseControl.Data.Entities.Admin
{
    public class EditUserData
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public URole Level { get; set; }

        public string Image { get; set; }
    }
}
