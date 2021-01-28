using Microsoft.EntityFrameworkCore;
using StudioGhibliAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudioGhibliAPI.Context
{
    public class GhibliDbContext : DbContext
    {
        public GhibliDbContext()
        {
        }

        public GhibliDbContext(DbContextOptions<GhibliDbContext> options) : base(options)
        {

        }

        public DbSet<Film> Film { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsbuilder)
        {

        }

       

    }
}
