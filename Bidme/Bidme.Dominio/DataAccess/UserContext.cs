using Bidme.Dominio.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Bidme.Dominio.DataAccess
{
    public class UserContext : IdentityDbContext<User>
    {
        #region CONSTRUCTORs
        //public UserContext() : base("name=BidmeContext") { }
        #endregion

        #region METHODs
        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //    modelBuilder.Entity<IdentityUser>()
        //        .ToTable("Usuarios");
        //}
        #endregion

    }
}
