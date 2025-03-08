using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.SqlServer;

using DummyProjectModel.Enitity;
using System.Data.Common;


namespace DummyProjectDAL.AppDbContext
{
    public class AppDbContexts :DbContext
    {

        public AppDbContexts(DbContextOptions<AppDbContexts> optionsBuilder) : base(optionsBuilder)
        {

        }
       public DbSet<Registration> Registration {  get; set; } 
        public DbSet<Login> Login {  get; set; } 

    }
}
