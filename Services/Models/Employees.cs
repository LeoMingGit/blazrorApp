using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models
{
    [SugarTable("Employees")]
    public class Employees
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int EmployeeId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string HomePhone { get; set; }

        public bool Active { get; set; }
    }
}
