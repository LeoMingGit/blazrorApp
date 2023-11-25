using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models
{
    [SugarTable("EmployeeSkills")]
    public class EmployeeSkills
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int EmployeeSkillID { get; set; }

        public int EmployeeID { get; set; }

        public int SkillID { get; set; }

        public int Level { get; set; }

        public int YearsOfExperience { get; set; }

        public decimal HourlyWage { get; set; }
    }
}
