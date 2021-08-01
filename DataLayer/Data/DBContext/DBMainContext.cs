using DataLayer.Data.Models; 
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace DataLayer.Data.DBContext
{
    public class DBMainContext : IdentityDbContext<IdentityUser>
    {
        public DBMainContext(DbContextOptions<DBMainContext> options) : base(options)
        {

        }

        public DbSet<TBProducts> Products { get; set; }
        public DbSet<TBOrders> Orders { get; set; }
    }
}
