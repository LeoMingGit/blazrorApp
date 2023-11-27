using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Services.DAL
{

    public class EmployeeContext : DbContext
    {

        public EmployeeContext(DbContextOptions<EmployeeContext> options)
       : base(options)
        {
        }


        public virtual DbSet<Employees> Employees { get; set; }

        public virtual DbSet<EmployeeSkills> EmployeeSkills { get; set; }


        public virtual DbSet<Skills> Skills { get; set; }

    }
}
