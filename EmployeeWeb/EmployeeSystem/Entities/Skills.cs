using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeSystem.Entities
{

    [Table("Skills")]
    public class Skills
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int SkillID { get; set; }

        public string Description { get; set; }
        /// <summary>
        /// True if some kind of Journeyman or other certification is required for this skill
        /// </summary>
        public bool RequiresTicket { get; set; }
    }
}
