using eUseControl.Data.Entities.Product;
using eUseControl.Data.Entities.User;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUseControl.BussinesLogic.AppBL
{
    public class TableContext : DbContext
    {
        public TableContext() : base("name=bookify")
        {
        }
        public virtual DbSet<UserTable> Users { get; set; }
        public virtual DbSet<Session> Sessions { get; set; }
        public virtual DbSet<BookTable> Books { get; set; }
    }
}
