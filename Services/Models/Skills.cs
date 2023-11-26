using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models
{

    [SugarTable("Skills")]
    public class Skills
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int SkillID { get; set; }

        public string Description { get; set; }
        /// <summary>
        /// True if some kind of Journeyman or other certification is required for this skill
        /// </summary>
        public bool RequiresTicket { get; set; }
    }
}
