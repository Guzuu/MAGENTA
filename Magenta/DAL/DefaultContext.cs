using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Magenta.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Magenta.DAL
{
    public class DefaultContext : IdentityDbContext
    {
        public DefaultContext()
        {

        }
        public DefaultContext(DbContextOptions<DefaultContext> options) : base(options)
        {

        }

        public DbSet<Machines> Machines { get; set; }
        public DbSet<WorkTypes> WorkTypes { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<Projects> Projects { get; set; }
        public DbSet<Works> Works { get; set; }
        public DbSet<Designs> Designs { get; set; }
    }
}
