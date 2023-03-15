using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CtrlS.Models
{
    public class CtrlSDbContext :DbContext
    {
        public CtrlSDbContext() : base("name=CtrlSConnection")
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        public System.Data.Entity.DbSet<CtrlS.Models.Blog> Blogs { get; set; }

        public System.Data.Entity.DbSet<CtrlS.Models.Contact> Contacts { get; set; }

        public System.Data.Entity.DbSet<CtrlS.Models.Client> Clients { get; set; }

        public System.Data.Entity.DbSet<CtrlS.Models.Talent> Talents { get; set; }

        public System.Data.Entity.DbSet<CtrlS.Models.Event> Events { get; set; }

        public System.Data.Entity.DbSet<CtrlS.Models.Video> Videos { get; set; }
    }
}