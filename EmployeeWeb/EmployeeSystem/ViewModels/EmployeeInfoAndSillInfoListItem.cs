using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeSystem.ViewModels
{
    public class EmployeeInfoAndSillInfoListItem
    {

        public string  FirstName { get; set; } 

        public string LastName { get; set; } 
      
        public string HomePhone { get; set; } 

        public  int  EmployeeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LevelName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int SkillID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Skill { get; set; }

        /// <summary>
        /// /
        /// </summary>
        public int YearsOfExperience { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal HourlyWage { get; set; }

    }
}
