using eUseControl.BussinesLogic.AppBL;
using eUseControl.BussinesLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUseControl.BussinesLogic
{
    public class BussinessLogic
    {
        public ISession GetSessionBL()
        {
            return new SessionBL();
        }

        public IAdmin GetAdminBL()
        {
            return new AdminBL();
        }
    }
}
